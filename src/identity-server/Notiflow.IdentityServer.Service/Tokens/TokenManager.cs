
namespace Notiflow.IdentityServer.Service.Tokens;

internal sealed class TokenManager : ITokenService
{
    private readonly JwtTokenSetting _jwtTokenSetting;

    public TokenManager(IOptions<JwtTokenSetting> jwtTokenSetting)
    {
        _jwtTokenSetting = jwtTokenSetting.Value;
    }

    public TokenResponse CreateToken(User user)
    {
        DateTime accessTokenExpiration = DateTime.Now.AddHours(_jwtTokenSetting.AccessTokenExpirationMinute);
        DateTime refreshTokenExpiration = DateTime.Now.AddHours(_jwtTokenSetting.RefreshTokenExpirationMinute);

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtTokenSetting.SecurityKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512Signature);

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

        return tokenResponse;
    }

    private static List<Claim> SetUserClaims(User user, IEnumerable<string> audiences)
    {
        List<Claim> claims = [];
        claims.AddJti();
        claims.AddIat();

        claims.AddNameIdentifier($"{user.Id}");
        claims.AddName(user.Name);
        claims.AddSurname(user.Surname);
        claims.AddEmail(user.Email);
        claims.AddAudiences(audiences);
        claims.AddGroupSid($"{user.TenantId}");

        return claims;
    }
}