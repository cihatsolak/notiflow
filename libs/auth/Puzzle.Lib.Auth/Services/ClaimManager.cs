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

    private int GetNameIdentifier()
    {
        string nameIdentifier = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        if (string.IsNullOrWhiteSpace(nameIdentifier))
            throw new JwtClaimException(nameof(nameIdentifier));

        bool succeeded = int.TryParse(nameIdentifier, out int identifier);
        if (!succeeded || 0 >= identifier)
            throw new JwtClaimException(nameof(nameIdentifier));

        return identifier;
    }

    private DateTime GetIat()
    {
        string issuedAtValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Iat))?.Value;
        if (!DateTime.TryParse(issuedAtValue, out DateTime issuedAt))
            throw new JwtClaimException(nameof(issuedAtValue));

        return issuedAt;
    }

    private DateTime GetBirthDate()
    {
        string birthDateValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Birthdate))?.Value;
        if (!DateTime.TryParse(birthDateValue, out DateTime birthDate))
            throw new JwtClaimException(nameof(birthDate));

        return birthDate;
    }
}
