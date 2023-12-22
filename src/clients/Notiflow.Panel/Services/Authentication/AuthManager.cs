namespace Notiflow.Panel.Services.Authentication;

public sealed class AuthManager(IHttpContextAccessor httpContextAccessor, IRestService restService) : IAuthService
{
    public async Task<bool> SignInAsync(SignInInput signInInput, CancellationToken cancellationToken)
    {
        var tokenResult = await restService.PostResponseAsync<Response<Token>>(nameof(AuthManager), "/user-service/auth/create-access-token", signInInput, cancellationToken);
        if (tokenResult.IsFailure)
        {
            return default;
        }

        var credentialCollection = HttpClientHeaderExtensions.CreateCollectionForBearerToken(tokenResult.Data.AccessToken);
        var userResult = await restService.GetResponseAsync<Response<User>>(nameof(AuthManager), "/user-service/auth/user", credentialCollection, cancellationToken);
        if (userResult.IsFailure)
        {
            return default;
        }

        List<Claim> userClaims =
        [
            new Claim(ClaimTypes.NameIdentifier, $"{userResult.Data.Id}", ClaimValueTypes.Integer),
            new Claim(ClaimTypes.Name, userResult.Data.Name, ClaimValueTypes.String),
            new Claim(ClaimTypes.Surname, userResult.Data.Surname, ClaimValueTypes.String),
            new Claim(ClaimTypes.Email, userResult.Data.Email, ClaimValueTypes.String)
        ];

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

        await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

        return true;
    }

    public async Task<Token> GetAccessTokenByRefreshTokenAsync(CancellationToken cancellationToken)
    {
        string refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
        if (string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException();
        }

        var tokenResult = await restService.PostResponseAsync<Response<Token>>("notiflow.api", "/user-service/auth/create-refresh-token", new { token = refreshToken }, cancellationToken);
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

        AuthenticateResult authenticateResult = await httpContextAccessor.HttpContext.AuthenticateAsync();
        AuthenticationProperties authenticationProperties = authenticateResult.Properties;
        authenticationProperties.StoreTokens(authenticationTokens);

        await httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticateResult.Principal, authenticationProperties);

        return tokenResult.Data;
    }

    public async Task RevokeRefreshTokenAsync(CancellationToken cancellationToken)
    {
        string accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        string refreshToken = await httpContextAccessor.HttpContext.GetTokenAsync("refresh_token");
        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            throw new UnauthorizedAccessException();
        }

        var credentials = HttpClientHeaderExtensions.CreateCollectionForBearerToken(accessToken);
        var revokeRefrestToken = await restService.DeleteApiResponseAsync<Response>("notiflow.api", $"/user-service/auth/revoke-refresh-token/{refreshToken}", credentials, cancellationToken);
        if (revokeRefrestToken.IsFailure)
        {
            throw new UnauthorizedAccessException();
        }
    }

    public async Task SignOutAsync(CancellationToken cancellationToken)
    {
        await RevokeRefreshTokenAsync(cancellationToken);
        await httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
