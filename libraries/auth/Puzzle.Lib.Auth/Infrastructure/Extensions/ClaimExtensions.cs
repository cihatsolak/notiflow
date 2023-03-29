namespace Puzzle.Lib.Auth.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for adding various types of claims to a collection of claims.
    /// </summary>
    public static class ClaimExtensions
    {
        /// <summary>
        /// Adds an email claim to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="email">The email address to add as a claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the email address is null or empty.</exception>
        public static void AddEmail(this ICollection<Claim> claims, string email)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(email);

            claims.Add(new Claim(ClaimTypes.Email, email, ClaimValueTypes.Email));
        }

        /// <summary>
        /// Adds a name claim to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="name">The name to add as a claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the name is null or empty.</exception>
        public static void AddName(this ICollection<Claim> claims, string name)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(name);

            claims.Add(new Claim(ClaimTypes.Name, name, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds a surname claim to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="surname">The surname to add as a claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the surname is null or empty.</exception>
        public static void AddSurname(this ICollection<Claim> claims, string surname)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(surname);

            claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, surname, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds a name identifier claim to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="nameIdentifier">The name identifier to add as a claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the name identifier is null or empty.</exception>
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(nameIdentifier);

            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier, ClaimValueTypes.Integer));
        }

        /// <summary>
        /// Adds a role claim to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="role">The role to add as a claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection is null.</exception>
        /// <exception cref="ArgumentException">Thrown when the role is null or empty.</exception>
        public static void AddRole(this ICollection<Claim> claims, string role)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(role);

            claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds multiple role claims to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims.</param>
        /// <param name="roles">The roles to add as claims.</param>
        /// <exception cref="ArgumentNullException">Thrown when the claims collection or the roles list is null.</exception>
        public static void AddRoles(this List<Claim> claims, IEnumerable<string> roles)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentNullException.ThrowIfNull(roles);

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role, ClaimValueTypes.String)));
        }

        /// <summary>
        /// Adds a new "jti" (JWT ID) claim to the collection of claims. The "jti" claim provides a unique identifier for the JWT.
        /// </summary>
        /// <param name="claims">The collection of claims to which the new claim will be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when the 'claims' parameter is null.</exception>
        public static void AddJti(this ICollection<Claim> claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        }

        /// <summary>
        /// Adds a new "aud" (audience) claim to the collection of claims. The "aud" claim identifies the intended audience for the JWT.
        /// </summary>
        /// <param name="claims">The collection of claims to which the new claim will be added.</param>
        /// <param name="audience">The audience to be added as a new claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the 'claims' parameter is null or when the 'audience' parameter is null or empty.</exception>
        public static void AddAudience(this List<Claim> claims, string audience)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(audience);

            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, audience, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds a list of "aud" (audience) claims to the collection of claims. The "aud" claim identifies the intended audience for the JWT.
        /// </summary>
        /// <param name="claims">The collection of claims to which the new claims will be added.</param>
        /// <param name="audiences">The list of audiences to be added as new claims.</param>
        /// <exception cref="ArgumentNullException">Thrown when the 'claims' parameter is null or when the 'audiences' parameter is null.</exception>
        public static void AddAudiences(this List<Claim> claims, IEnumerable<string> audiences)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentNullException.ThrowIfNull(audiences);

            claims.AddRange(audiences.Select(audience => new Claim(JwtRegisteredClaimNames.Aud, audience, ClaimValueTypes.String)));
        }

        /// <summary>
        /// Adds a claim representing the username of a user to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims to which the username claim will be added.</param>
        /// <param name="username">The username to be represented by the claim.</param>
        /// <exception cref="ArgumentNullException">Thrown if the claims parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the username parameter is null or empty.</exception>
        public static void AddUsername(this ICollection<Claim> claims, string username)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(username);

            claims.Add(new Claim(ClaimTypes.GivenName, username, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds a claim representing the security identifier (SID) of a group to a collection of claims.
        /// </summary>
        /// <param name="claims">The collection of claims to which the group SID claim will be added.</param>
        /// <param name="groupSid">The security identifier (SID) of the group to be represented by the claim.</param>
        /// <exception cref="ArgumentNullException">Thrown if the claims parameter is null.</exception>
        /// <exception cref="ArgumentException">Thrown if the groupSid parameter is null or empty.</exception>
        public static void AddGroupSid(this ICollection<Claim> claims, string groupSid)
        {
            ArgumentNullException.ThrowIfNull(claims);
            ArgumentException.ThrowIfNullOrEmpty(groupSid);

            claims.Add(new Claim(ClaimTypes.GroupSid, groupSid, ClaimValueTypes.String));
        }

        /// <summary>
        /// Adds a new "iat" (issued at) claim to the collection of claims. The "iat" claim identifies the time at which the JWT was issued.
        /// </summary>
        /// <param name="claims">The collection of claims to which the new claim will be added.</param>
        /// <exception cref="ArgumentNullException">Thrown when the 'claims' parameter is null.</exception>
        public static void AddIat(this ICollection<Claim> claims)
        {
            ArgumentNullException.ThrowIfNull(claims);

            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString(), ClaimValueTypes.DateTime));
        }

        /// <summary>
        /// Adds a new "birthdate" claim to the collection of claims. The "birthdate" claim identifies the birthdate of the subject of the JWT.
        /// </summary>
        /// <param name="claims">The collection of claims to which the new claim will be added.</param>
        /// <param name="birthDate">The birthdate to be added as a new claim.</param>
        /// <exception cref="ArgumentNullException">Thrown when the 'claims' parameter is null.</exception>
        public static void AddBirthDate(this ICollection<Claim> claims, DateTime birthDate)
        {
            ArgumentNullException.ThrowIfNull(claims);

            claims.Add(new Claim(JwtRegisteredClaimNames.Birthdate, birthDate.ToString(), ClaimValueTypes.DateTime));
        }
    }
}
