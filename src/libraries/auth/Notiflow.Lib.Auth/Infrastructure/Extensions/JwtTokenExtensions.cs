﻿namespace Notiflow.Lib.Auth.Infrastructure.Extensions
{
    public static class JwtTokenExtensions
    {
        /// <summary>
        /// Create security key
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>type of security key</returns>
        /// <exception cref="ArgumentNullException">thrown when key is empty or null</exception>
        public static SecurityKey CreateSecurityKey(string key)
        {
            ArgumentException.ThrowIfNullOrEmpty(key);

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        /// <summary>
        /// Create signing credentials
        /// </summary>
        /// <param name="securityKey">security key</param>
        /// <returns>type of signing credentials</returns>
        /// <exception cref="ArgumentNullException">thrown when security key is null</exception>
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
        {
            ArgumentNullException.ThrowIfNull(securityKey);

            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }

        /// <summary>
        /// Create refresh token
        /// </summary>
        /// <returns>type of base 64 string</returns>
        public static string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte).Replace("/", "+");
        }
    }
}