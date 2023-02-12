namespace Notiflow.Lib.Auth.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Get all claim value by claim type
        /// </summary>
        /// <remarks>Lists values based on the specified claim type. For example: Role</remarks>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <param name="claimType">claim type name</param>
        /// <returns>claim value list</returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null or claim type is null|empty</exception>
        public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            ArgumentException.ThrowIfNullOrEmpty(claimType);
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.FindAll(claimType).Select(x => x.Value);
        }

        /// <summary>
        /// Get claim roles
        /// </summary>
        /// <remarks>lists claim roles</remarks>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <returns>role list</returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null</exception>
        public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(ClaimTypes.Role);
        }

        /// <summary>
        /// Get claim audiences
        /// </summary>
        /// <remarks>lists audiences</remarks>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <returns>lists audiences</returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null</exception>
        public static IEnumerable<string> ClaimAudiences(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(JwtRegisteredClaimNames.Aud);
        }
    }
}
