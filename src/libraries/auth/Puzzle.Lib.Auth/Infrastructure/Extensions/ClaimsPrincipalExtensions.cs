namespace Puzzle.Lib.Auth.Infrastructure.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="ClaimsPrincipal"/> class to simplify access to claims.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Retrieves the values of the specified claim type from the current <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <param name="claimType">The type of the claim to retrieve.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of strings containing the values of the specified claim type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="claimsPrincipal"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="claimType"/> is null or empty.</exception>
        public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);
            ArgumentException.ThrowIfNullOrEmpty(claimType);
           
            return claimsPrincipal.FindAll(claimType).Select(x => x.Value);
        }

        /// <summary>
        /// Retrieves the values of the "role" claim type from the current <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of strings containing the values of the "role" claim type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="claimsPrincipal"/> is null.</exception>
        public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(ClaimTypes.Role);
        }

        /// <summary>
        /// Retrieves the values of the "aud" claim type from the current <see cref="ClaimsPrincipal"/>.
        /// </summary>
        /// <param name="claimsPrincipal">The <see cref="ClaimsPrincipal"/> instance.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of strings containing the values of the "aud" claim type.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="claimsPrincipal"/> is null.</exception>
        public static IEnumerable<string> ClaimAudiences(this ClaimsPrincipal claimsPrincipal)
        {
            ArgumentNullException.ThrowIfNull(claimsPrincipal);

            return claimsPrincipal.Claims(JwtRegisteredClaimNames.Aud);
        }
    }
}
