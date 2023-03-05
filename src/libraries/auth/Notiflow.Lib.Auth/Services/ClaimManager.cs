namespace Notiflow.Lib.Auth.Services
{
    public sealed class ClaimManager : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string EmailAddress => GetEmailAddress();
        public string Name => GetName();
        public int UserId => GetUserId();
        public string Role => GetRole();
        public List<string> Roles => GetRoles();
        public string Jti => GetJti();
        public List<string> Audiences => GetAudiences();
        public string Audience => GetAudience();
        public string Username => GetUsername();

        private string GetEmailAddress()
        {
            string emailAddress = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Email))?.Value;
            if (string.IsNullOrWhiteSpace(emailAddress))
                throw new ClaimException(ExceptionMessage.ClaimTypeEmailRequired);

            return emailAddress;
        }

        private string GetName()
        {
            string name = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Name))?.Value;
            if (string.IsNullOrWhiteSpace(name))
                throw new ClaimException(ExceptionMessage.ClaimTypeNameRequired);

            return name;
        }

        private int GetUserId()
        {
            string nameIdentifier = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            if (string.IsNullOrWhiteSpace(nameIdentifier))
                throw new ClaimException(ExceptionMessage.ClaimTypeNameIdentifierRequired);

            bool result = int.TryParse(nameIdentifier, out int userId);
            if (!result || 0 >= userId)
                throw new ClaimException(ExceptionMessage.ClaimTypeNameIdentifierRequired);

            return userId;
        }

        private string GetRole()
        {
            string roleName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.Role))?.Value;
            if (string.IsNullOrWhiteSpace(roleName))
                throw new ClaimException(ExceptionMessage.ClaimTypeRoleRequired);

            return roleName;
        }

        private List<string> GetRoles()
        {
            List<string> roles = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(ClaimTypes.Role))
                    .Select(p => p.Value).ToList();

            if (roles is null || !roles.Any())
                throw new ClaimException(ExceptionMessage.ClaimTypeRoleRequired);

            return roles;
        }


        private string GetJti()
        {
            string jti = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Jti))?.Value;
            if (string.IsNullOrWhiteSpace(jti))
                throw new ClaimException(ExceptionMessage.ClaimTypeJtiRequired);

            return jti;
        }

        private List<string> GetAudiences()
        {
            List<string> audiences = _httpContextAccessor.HttpContext.User.Claims
                    .Where(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))
                    .Select(p => p.Value).ToList();

            if (audiences is null || !audiences.Any())
                throw new ClaimException(ExceptionMessage.ClaimTypeAudienceRequired);

            return audiences;
        }

        private string GetAudience()
        {
            string audience = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(JwtRegisteredClaimNames.Aud))?.Value;
            if (string.IsNullOrWhiteSpace(audience))
                throw new ClaimException(ExceptionMessage.ClaimTypeAudienceRequired);

            return audience;
        }

        private string GetUsername()
        {
            string username = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(p => p.Type.Equals(ClaimTypes.GivenName))?.Value;
            if (string.IsNullOrWhiteSpace(username))
                throw new ClaimException(ExceptionMessage.ClaimTypeUsernameRequired);

            return username;
        }
    }
}
