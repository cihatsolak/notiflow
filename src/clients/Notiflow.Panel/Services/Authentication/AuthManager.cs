namespace Notiflow.Panel.Services.Authentication;

public sealed class AuthManager : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRestService _restService;

    public AuthManager(
        IHttpContextAccessor httpContextAccessor,
        IRestService restService)
    {
        _httpContextAccessor = httpContextAccessor;
        _restService = restService;
    }

    public async Task<bool> SignInAsync(SignInInput signInInput, CancellationToken cancellationToken)
    {
        var tokenResult = await _restService.PostResponseAsync<Response<Token>>(nameof(AuthManager), "/user-service/auth/create-access-token", signInInput, cancellationToken);
        if (tokenResult.IsFailure)
        {
            return default;
        }

        var credentialCollection = HttpClientHeaderExtensions.CreateCollectionForBearerToken(tokenResult.Data.AccessToken);
        var userResult = await _restService.GetResponseAsync<Response<User>>(nameof(AuthManager), "/user-service/auth/user", credentialCollection, cancellationToken);
        if (userResult.IsFailure)
        {
            return default;
        }

        List<Claim> userClaims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, $"{userResult.Data.Id}", ClaimValueTypes.Integer),
            new Claim(ClaimTypes.Name, userResult.Data.Name, ClaimValueTypes.String),
            new Claim(ClaimTypes.GivenName, userResult.Data.Surname, ClaimValueTypes.String),
            new Claim(ClaimTypes.Email, userResult.Data.Email, ClaimValueTypes.String)
        };

        ClaimsIdentity claimsIdentity = new(userClaims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
        ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

        AuthenticationProperties authenticationProperties = new();
        authenticationProperties.StoreTokens(new List<AuthenticationToken>()
            {
                new AuthenticationToken
                {
                    Name = "access_token",
                    Value = tokenResult.Data.AccessToken
                },
                new AuthenticationToken
                {
                    Name = "refresh_token",
                    Value = tokenResult.Data.RefreshToken
                },
                new AuthenticationToken
                {
                    Name = "expires_in",
                    Value = DateTime.Now.AddSeconds(tokenResult.Data.ExpiresIn).ToString("o", CultureInfo.InvariantCulture) //2023-11-11T09:48:58.3048790+00:00
                }
            });

        authenticationProperties.IsPersistent = signInInput.RememberMe;
        authenticationProperties.ExpiresUtc = tokenResult.Data.AccessTokenExpiration;

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

        return true;
    }

    public async Task<Token> GetAccessTokenByRefreshTokenAsync(CancellationToken cancellationToken)
    {
        string refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException();
        }

        var tokenResult = await _restService.PostResponseAsync<Response<Token>>("notiflow.api", "/user-service/auth/create-refresh-token", new { token = refreshToken }, cancellationToken);
        if (tokenResult.IsFailure)
        {
            throw new UnauthorizedAccessException();
        }

        List<AuthenticationToken> authenticationTokens = new()
        {
            new AuthenticationToken
            {
                Name = "access_token",
                Value = tokenResult.Data.AccessToken
            },
            new AuthenticationToken
            {
                Name = "refresh_token",
                Value = tokenResult.Data.RefreshToken
            },
            new AuthenticationToken
            {
                Name = "expires_in",
                Value = DateTime.Now.AddSeconds(tokenResult.Data.ExpiresIn).ToString("o", CultureInfo.InvariantCulture) //2023-11-11T09:48:58.3048790+00:00
            }
        };

        AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync();
        AuthenticationProperties authenticationProperties = authenticateResult.Properties;
        authenticationProperties.StoreTokens(authenticationTokens);

        await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal, authenticationProperties);

        return tokenResult.Data;
    }

    public async Task RevokeRefreshTokenAsync(CancellationToken cancellationToken)
    {
        string refreshToken = await _httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException();
        }

        var revokeRefrestToken = await _restService.DeleteApiResponseAsync<Response>("notiflow.api", $"/user-service/auth/revoke-refresh-token/{refreshToken}", cancellationToken);
        if (revokeRefrestToken.IsFailure)
        {
            throw new UnauthorizedAccessException();
        }
    }

    public async Task SignOutAsync(CancellationToken cancellationToken)
    {
        await RevokeRefreshTokenAsync(cancellationToken);
        await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
