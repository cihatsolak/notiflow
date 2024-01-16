namespace Puzzle.Lib.Auth.Services;

public sealed class ClaimManager : IClaimService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string Email => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Email))?.Value;
    public string Username => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.UniqueName))?.Value;
    public string Name => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Name))?.Value;
    public string FamilyName => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.FamilyName))?.Value;
    public int NameIdentifier => GetNameIdentifier();
    public string Role => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Role))?.Value;
    public IEnumerable<string> Roles => _httpContextAccessor.HttpContext.User.Claims.Where(p => p.Type.Equals(ClaimTypes.Role)).Select(p => p.Value);
    public string Jti => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Jti))?.Value;
    public string Audience => _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))?.Value;
    public IEnumerable<string> Audiences => _httpContextAccessor.HttpContext.User.Claims.Where(p => p.Type.Equals(JwtRegisteredClaimNames.Aud)).Select(p => p.Value);
    public string GivenName => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.GivenName))?.Value;
    public DateTime Iat => GetIat();
    public DateTime BirthDate => GetBirthDate();
    public string GroupSid => _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.GroupSid))?.Value;

    private int GetNameIdentifier()
    {
        string nameIdentifier = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (string.IsNullOrWhiteSpace(nameIdentifier))
            throw new SecurityTokenException("No name identifier was found in the token. token is invalid.");

        bool succeeded = int.TryParse(nameIdentifier, out int identifier);
        if (!succeeded || 0 >= identifier)
            throw new SecurityTokenException("No name identifier was found in the token. token is invalid.");

        return identifier;
    } 

    private DateTime GetIat()
    {
        string issuedAtValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Iat))?.Value;
        if (!DateTime.TryParse(issuedAtValue, CultureInfo.CurrentCulture, out DateTime issuedAt))
            throw new SecurityTokenException("No issued at value was found in the token. token is invalid.");

        return issuedAt;
    }

    private DateTime GetBirthDate()
    {
        string birthDateValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Birthdate))?.Value;
        if (!DateTime.TryParse(birthDateValue, CultureInfo.CurrentCulture, out DateTime birthDate))
            throw new SecurityTokenException("No date of birth was found in the token. token is invalid.");

        return birthDate;
    }
}
