namespace Puzzle.Lib.Auth.Infrastructure.Extensions
{
    /// <summary>
    ///  Extension methods for configure claim list in an <see cref="Claim" />.
    /// </summary>
    public static class ClaimExtensions
    {
        /// <summary>
        /// Add e-mail address to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="email">email address to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when email address is empty or null</exception>
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(email);

            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        /// <summary>
        /// Add name to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="name">name to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when name is empty or null</exception>
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(name);

            claims.Add(new Claim(ClaimTypes.Name, name));
        }

        /// <summary>
        /// Add name identifier to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="nameIdentifier">name identifier to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when name identifier is empty or null</exception>
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(nameIdentifier);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }

        /// <summary>
        /// Add role to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="role">role to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when role is empty or null</exception>
        public static void AddRole(this ICollection<Claim> claims, string role)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(role);

            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        /// <summary>
        /// Add roles to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="roles">roles to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentNullException">thrown when role is null</exception>
        public static void AddRoles(this List<Claim> claims, IEnumerable<string> roles)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentNullException.ThrowIfNull(roles);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        }

        /// <summary>
        /// Generate jti value and add to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        public static void AddJti(this ICollection<Claim> claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Add audience to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="audience">audience to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when audience is null or empty</exception>
        public static void AddAud(this List<Claim> claims, string audience)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(audience);

            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience));
        }

        /// <summary>
        /// Add audiences to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="audiences">audiences to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentNullException">thrown when audiences is null/exception>
        public static void AddAuds(this List<Claim> claims, List<string> audiences)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentNullException.ThrowIfNull(audiences);

            claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience)));
        }

        /// <summary>
        /// Add username to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="username">username to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when username is null or empty/exception>
        public static void AddUsername(this ICollection<Claim> claims, string username)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(username);

            claims.Add(new Claim(ClaimTypes.GivenName, username));
        }

        /// <summary>
        /// Add group sid to claim list
        /// </summary>
        /// <param name="claims">list of claims</param>
        /// <param name="groupSid">group sid to be added to the claim list</param>
        /// <exception cref="ArgumentNullException">thrown when the claim list is null</exception>
        /// <exception cref="ArgumentException">thrown when groupSid is null or empty/exception>
        public static void AddGroupSid(this ICollection<Claim> claims, string groupSid)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(groupSid);

            claims.Add(new Claim(ClaimTypes.GroupSid, groupSid));
        }
    }
}
