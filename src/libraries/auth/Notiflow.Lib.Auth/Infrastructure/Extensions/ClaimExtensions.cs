namespace Notiflow.Lib.Auth.Infrastructure.Extensions
{
    public static class ClaimExtensions
    {
        /// <summary>
        /// Add email for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="email">email address</param>
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        /// <summary>
        /// Add name for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="name">name</param>
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        /// <summary>
        /// Add name identifier for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="id">name identifier</param>
        public static void AddNameIdentifier(this ICollection<Claim> claims, string id)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, id));
        }

        /// <summary>
        /// Add role for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="role">role name</param>
        public static void AddRole(this ICollection<Claim> claims, string role)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        /// <summary>
        /// Add jti for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        public static void AddJti(this ICollection<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Add audiences for claims
        /// </summary>
        /// <param name="claims">type of claims list</param>
        /// <param name="audiences">audience list</param>
        public static void AddAuds(this List<Claim> claims, List<string> audiences)
        {
            claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));
        }

        /// <summary>
        /// Add audience for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="audience">audience</param>
        public static void AddAud(this List<Claim> claims, string audience)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        /// <summary>
        /// Add username for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="username">user name</param>
        public static void AddUsername(this ICollection<Claim> claims, string username)
        {
            claims.Add(new Claim(ClaimTypes.GivenName, username));
        }

        /// <summary>
        /// Add platform for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="platformType">platform type</param>
        public static void AddPlatform(this ICollection<Claim> claims, string platformType)
        {
            claims.Add(new Claim(ClaimTypes.GroupSid, platformType));
        }

        /// <summary>
        /// Add turkuaz web site number for claims
        /// </summary>
        /// <param name="claims">type of claims collection</param>
        /// <param name="webSiteNumber">web site number</param>
        public static void AddWebSiteNumber(this ICollection<Claim> claims, int webSiteNumber)
        {
            claims.Add(new Claim(ClaimTypes.Spn, $"{webSiteNumber}"));
        }
    }
}
