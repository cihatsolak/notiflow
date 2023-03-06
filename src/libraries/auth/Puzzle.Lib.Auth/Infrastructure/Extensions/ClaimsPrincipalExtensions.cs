namespace Puzzle.Lib.Auth.Infrastructure.Extensions
{
    /// <summary>
    /// Extension methods for configure claim principal in an <see cref="ClaimsPrincipal" />.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Lists claim values for claim type in claims principal
        /// </summary>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <param name="claimType">claim type</param>
        /// <returns>filtered claim value list</returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null or claim type is null|empty</exception>
        /// <exception cref="ArgumentException">thrown when claim type is empty or null</exception>
        public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);
            ArgumentException.ThrowIfNullOrEmpty(claimType);
           
            return claimsPrincipal.FindAll(claimType).Select(x => x.Value);
        }

        /// <summary>
        /// Lists roles in claims principal
        /// </summary>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <returns>role list</returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null</exception>
        public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(ClaimTypes.Role);
        }

        /// <summary>
        /// Lists audiences in claim principal
        /// </summary>
        /// <param name="claimsPrincipal">type of claims principal</param>
        /// <returns>audiences list </returns>
        /// <exception cref="ArgumentNullException">thrown when claims principal is null</exception>
        public static IEnumerable<string> ClaimAudiences(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(JwtRegisteredClaimNames.Aud);
        }
    }
}
