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
        public string Surname => GetSurname();
        public int UserId => GetUserId();
        public string Role => GetRole();
        public IEnumerable<string> Roles => GetRoles();
        public string Jti => GetJti();
        public IEnumerable<string> Audiences => GetAudiences();
        public string Audience => GetAudience();
        public string Username => GetUsername();
        public DateTime Iat => GetIat();
        public DateTime BirthDate => GetBirthDate();

        private string GetEmail()
        {
            string email = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Email))?.Value;
            if (string.IsNullOrWhiteSpace(email))
                throw new ClaimException(nameof(email));

            return email;
        }

        private string GetName()
        {
            string name = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.Name))?.Value;
            if (string.IsNullOrWhiteSpace(name))
                throw new ClaimException(nameof(name));

            return name;
        }

        private string GetSurname()
        {
            string familyName = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.FamilyName))?.Value;
            if (string.IsNullOrWhiteSpace(familyName))
                throw new ClaimException(nameof(familyName));

            return familyName;
        }

        private int GetUserId()
        {
            string nameIdentifier = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (string.IsNullOrWhiteSpace(nameIdentifier))
                throw new ClaimException(nameof(nameIdentifier));

            bool result = int.TryParse(nameIdentifier, out int userId);
            if (!result || 0 >= userId)
                throw new ClaimException(nameof(nameIdentifier));

            return userId;
        }

        private string GetRole()
        {
            string role = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Role))?.Value;
            if (string.IsNullOrWhiteSpace(role))
                throw new ClaimException(nameof(role));

            return role;
        }

        private IEnumerable<string> GetRoles()
        {
            IEnumerable<string> roles = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(ClaimTypes.Role))
                    .Select(p => p.Value);

            if (roles is null || !roles.Any())
                throw new ClaimException(nameof(roles));

            return roles;
        }


        private string GetJti()
        {
            string jti = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Jti))?.Value;
            if (string.IsNullOrWhiteSpace(jti))
                throw new ClaimException(nameof(jti));

            return jti;
        }

        private IEnumerable<string> GetAudiences()
        {
            IEnumerable<string> audiences = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))
                    .Select(p => p.Value);

            if (audiences is null || !audiences.Any())
                throw new ClaimException(nameof(audiences));

            return audiences;
        }

        private string GetAudience()
        {
            string audience = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))?.Value;
            if (string.IsNullOrWhiteSpace(audience))
                throw new ClaimException(nameof(audience));

            return audience;
        }

        private string GetUsername()
        {
            string givenName = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(p => p.Type.Equals(ClaimTypes.GivenName))?.Value;
            if (string.IsNullOrWhiteSpace(givenName))
                throw new ClaimException(nameof(givenName));

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
