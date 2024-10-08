﻿namespace Puzzle.Lib.Auth.Services;

public sealed class ClaimManager(IHttpContextAccessor httpContextAccessor) : IClaimService
{
    private const string ACCESS_EXCEPTION_MESSAGE = "The user does not have access permission.";
   
    public string Email => GetClaimValue<string>(ClaimTypes.Email);
    public string Username => GetClaimValue<string>(ClaimTypes.Upn);
    public string Name => GetClaimValue<string>(ClaimTypes.Name);
    public string Surname => GetClaimValue<string>(ClaimTypes.Surname);
    public int NameIdentifier => GetClaimValue<int>(ClaimTypes.NameIdentifier);
    public string Role => GetClaimValue<string>(ClaimTypes.Role);
    public IEnumerable<string> Roles => GetClaimValues(ClaimTypes.Role);
    public string Jti => GetClaimValue<string>(JwtRegisteredClaimNames.Jti);
    public string Audience => GetClaimValue<string>(JwtRegisteredClaimNames.Aud);
    public IEnumerable<string> Audiences => GetClaimValues(JwtRegisteredClaimNames.Aud);
    public string GivenName => GetClaimValue<string>(ClaimTypes.GivenName);
    public DateTime Iat => GetIat();
    public DateTime BirthDate => GetClaimValue<DateTime>(ClaimTypes.DateOfBirth);
    public string GroupSid => GetClaimValue<string>(ClaimTypes.GroupSid);

    private bool IsUserAuthenticated => httpContextAccessor?.HttpContext?.User?.Identity?.IsAuthenticated == true;

    private DateTime GetIat()
    {
        if (!IsUserAuthenticated)
            throw new UnauthorizedAccessException(ACCESS_EXCEPTION_MESSAGE);

        string issuedAtValue = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type.Equals(JwtRegisteredClaimNames.Iat))?.Value;
        if (!long.TryParse(issuedAtValue, CultureInfo.InvariantCulture, out long epochTime))
        {
            throw new SecurityTokenException("No issued at value was found in the token. token is invalid.");
        }

        return DateTimeOffset.FromUnixTimeSeconds(epochTime).DateTime;
    }

    private Type GetClaimValue<Type>(string claimType)
    {
        if (!IsUserAuthenticated)
            throw new UnauthorizedAccessException(ACCESS_EXCEPTION_MESSAGE);

        string claimValue = httpContextAccessor.HttpContext.User.FindFirstValue(claimType);
        if (string.IsNullOrWhiteSpace(claimValue))
            throw new SecurityTokenDecompressionFailedException($"No {claimType} was found in the token. token is invalid.");

        return (Type)Convert.ChangeType(claimValue, typeof(Type), CultureInfo.CurrentCulture);
    }

    private IEnumerable<string> GetClaimValues(string claimType)
    {
        if (!IsUserAuthenticated)
            throw new UnauthorizedAccessException(ACCESS_EXCEPTION_MESSAGE);

        IEnumerable<Claim> claims = httpContextAccessor.HttpContext?.User?.Claims?.Where(claim => claim.Type.Equals(claimType, StringComparison.OrdinalIgnoreCase));
        if (claims.IsNullOrEmpty())
            throw new SecurityTokenDecompressionFailedException($"No {claimType} was found in the token. token is invalid.");

        return claims.Select(claim => claim.Value);
    }
}
