using Puzzle.Lib.Auth.Infrastructure;

namespace Notiflow.IdentityServer.Service.Tokens;

internal sealed class TokenManager : ITokenService
{
    private readonly JwtTokenSetting _jwtTokenSetting;

    public TokenManager(IOptions<JwtTokenSetting> jwtTokenSetting)
    {
        _jwtTokenSetting = jwtTokenSetting.Value;
    }

    public Result<TokenResponse> CreateToken(User user)
    {
        DateTime accessTokenExpiration = DateTime.Now.AddHours(_jwtTokenSetting.AccessTokenExpirationMinute);
        DateTime refreshTokenExpiration = DateTime.Now.AddHours(_jwtTokenSetting.RefreshTokenExpirationMinute);

        SecurityKey securityKey = JwtTokenExtensions.CreateSecurityKey(_jwtTokenSetting.SecurityKey);
        SigningCredentials signingCredentials = JwtTokenExtensions.CreateSigningCredentials(securityKey);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: _jwtTokenSetting.Issuer,
            expires: accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetUserClaims(user, _jwtTokenSetting.Audiences),
            signingCredentials: signingCredentials
        );

        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        string accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

        TokenResponse tokenResponse = new()
        {
            AccessToken = accessToken,
            AccessTokenExpiration = accessTokenExpiration,
            ExpiresIn = (int)(accessTokenExpiration - DateTime.Now.AddSeconds(30)).TotalSeconds,
            RefreshToken = JwtTokenExtensions.CreateRefreshToken(),
            RefreshTokenExpiration = refreshTokenExpiration
        };

        return Result<TokenResponse>.Success(tokenResponse);
    }

    private static IEnumerable<Claim> SetUserClaims(User user, IEnumerable<string> audiences)
    {
        List<Claim> claims = new();
        claims.AddJti();
        claims.AddIat();

        claims.AddNameIdentifier(user.Id.ToString());
        claims.AddName(user.Name);
        claims.AddFamilyName(user.Surname);
        claims.AddEmail(user.Email);
        claims.AddAudiences(audiences);

        return claims;
    }
}