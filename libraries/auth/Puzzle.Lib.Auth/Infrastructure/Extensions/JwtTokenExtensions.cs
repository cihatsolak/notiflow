namespace Puzzle.Lib.Auth.Infrastructure.Extensions
{
    /// <summary>
    /// Contains extension methods for creating and managing JWT tokens.
    /// </summary>
    public static class JwtTokenExtensions
    {
        /// <summary>
        /// Creates a <see cref="SecurityKey"/> instance from the provided key string.
        /// </summary>
        /// <param name="key">The key used to create the <see cref="SecurityKey"/> instance.</param>
        /// <returns>A new <see cref="SecurityKey"/> instance.</returns>
        /// <exception cref="ArgumentException">Thrown when the <paramref name="key"/> parameter is null or empty.</exception>
        public static SecurityKey CreateSecurityKey(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// Creates a <see cref="SigningCredentials"/> instance from the provided <see cref="SecurityKey"/> instance.
        /// </summary>
        /// <param name="securityKey">The <see cref="SecurityKey"/> instance used to create the <see cref="SigningCredentials"/> instance.</param>
        /// <returns>A new <see cref="SigningCredentials"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="securityKey"/> parameter is null.</exception>
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            ArgumentNullException.ThrowIfNull(securityKey);

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

        /// <summary>
        /// Creates a new refresh token string with a length of 32 bytes.
        /// </summary>
        /// <returns>A new refresh token string.</returns>
        public static string CreateRefreshToken()
        {
            var number = new byte[32];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(number);

            return Convert.ToBase64String(number);
        }
    }
}