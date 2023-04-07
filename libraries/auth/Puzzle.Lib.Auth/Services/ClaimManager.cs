namespace Puzzle.Lib.Auth.Services
{
    public sealed class ClaimManager : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email => GetEmail();
        public string Name => GetName();
        public string FamilyName => GetFamilyName();
        public int NameIdentifier => GetNameIdentifier();
        public string Role => GetRole();
        public IEnumerable<string> Roles => GetRoles();
        public string Jti => GetJti();
        public string Audience => GetAudience();
        public IEnumerable<string> Audiences => GetAudiences();
        public string GivenName => GetGivenName();
        public DateTime Iat => GetIat();
        public DateTime BirthDate => GetBirthDate();

        private string GetEmail()
        {
            string email = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Email))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(email);

            return email;
        }

        private string GetName()
        {
            string name = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Name))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(name);

            return name;
        }

        private string GetFamilyName()
        {
            string familyName = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.FamilyName))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(familyName);

            return familyName;
        }

        private int GetNameIdentifier()
        {
            string nameIdentifier = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(nameIdentifier);

            bool succeeded = int.TryParse(nameIdentifier, out int identifier);
            if (!succeeded || 0 >= identifier)
                throw new ClaimException(nameof(nameIdentifier));

            return identifier;
        }

        private string GetRole()
        {
            string role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Role))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(role);

            return role;
        }

        private IEnumerable<string> GetRoles()
        {
            IEnumerable<string> roles = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(ClaimTypes.Role))
                    .Select(p => p.Value);

            AuthArgumentException.ThrowIfNullOrEmpty(roles);

            return roles;
        }


        private string GetJti()
        {
            string jti = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Jti))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(jti);

            return jti;
        }

        private string GetAudience()
        {
            string audience = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(audience);

            return audience;
        }

        private IEnumerable<string> GetAudiences()
        {
            IEnumerable<string> audiences = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))
                    .Select(p => p.Value);

            AuthArgumentException.ThrowIfNullOrEmpty(audiences);

            return audiences;
        }

        private string GetGivenName()
        {
            string givenName = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.GivenName))?.Value;
            AuthArgumentException.ThrowIfNullOrEmpty(givenName);

            return givenName;
        }

        private DateTime GetIat()
        {
            string issuedAtValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Iat))?.Value;
            if (!DateTime.TryParse(issuedAtValue, out DateTime issuedAt))
                throw new ClaimException(nameof(issuedAtValue));

            return issuedAt;
        }

        private DateTime GetBirthDate()
        {
            string birthDateValue = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Birthdate))?.Value;
            if (!DateTime.TryParse(birthDateValue, out DateTime birthDate))
                throw new ClaimException(nameof(birthDate));

            return birthDate;
        }
    }
}
