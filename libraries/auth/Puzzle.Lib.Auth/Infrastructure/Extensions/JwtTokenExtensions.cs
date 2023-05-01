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
        /// Creates a refresh token with the specified size.
        /// </summary>
        /// <param name="size">The size of the refresh token to be created. Default value is 32.</param>
        /// <returns>A string representation of the created refresh token.</returns>
        public static string CreateRefreshToken(int size = 32)
        {
            var number = new byte[size];
            using var randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(number);

            return Convert.ToBase64String(number)
                .Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", string.Empty);
        }
    }
}