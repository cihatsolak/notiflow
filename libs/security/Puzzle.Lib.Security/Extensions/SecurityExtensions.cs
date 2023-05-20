namespace Puzzle.Lib.Security.Extensions
{
    /// <summary>
    /// Provides extension methods for security related functionalities such as password hashing, encryption, and decryption.
    /// </summary>
    public static class SecurityExtensions
    {
        /// <summary>
        /// Hashes a given password using the BCrypt algorithm.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password.</returns>
        /// <exception cref="ArgumentException">Thrown when the password parameter is null or empty.</exception>
        public static string CreatePasswordHash(this string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);

            return Crypto.HashPassword(password);
        }

        /// <summary>
        /// Verifies if a given plain text password matches with a given hashed password.
        /// </summary>
        /// <param name="hashedPassword">The hashed password to compare.</param>
        /// <param name="password">The plain text password to compare.</param>
        /// <returns>True if the passwords match, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown when the hashedPassword or password parameters are null or empty.</exception>
        public static bool VerifyPasswordHash(this string hashedPassword, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(hashedPassword);
            ArgumentException.ThrowIfNullOrEmpty(password);

            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }

        /// <summary>
        /// Encrypts a given plain text using the AES encryption algorithm.
        /// </summary>
        /// <param name="text">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        /// <exception cref="ArgumentException">Thrown when the text parameter is null or empty.</exception>
        public static string Encrypt(this string text)
        {
            ArgumentException.ThrowIfNullOrEmpty(text);

            byte[] key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using Aes aes = Aes.Create();
            using ICryptoTransform cryptoTransform = aes.CreateEncryptor(key, aes.IV);
            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(text);
            }

            byte[] iv = aes.IV;
            byte[] decryptedContent = memoryStream.ToArray();
            byte[] result = new byte[iv.Length + decryptedContent.Length];

            Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
            Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// Decrypts a given cipher text using the AES decryption algorithm.
        /// </summary>
        /// <param name="cipherText">The cipher text to decrypt.</param>
        /// <returns>The decrypted text.</returns>
        /// <exception cref="ArgumentException">Thrown when the cipherText parameter is null or empty.</exception>
        public static string Decrypt(this string cipherText)
        {
            ArgumentException.ThrowIfNullOrEmpty(cipherText);

            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - iv.Length];

            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

            byte[] key = Encoding.UTF8.GetBytes("E546C8DF278CD5931069B522E695D4F2");

            using Aes aes = Aes.Create();
            using ICryptoTransform cryptoTransform = aes.CreateDecryptor(key, iv);

            string text;
            using (MemoryStream memoryStream = new(cipher))
            {
                using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Read);
                using StreamReader streamWriter = new(cryptoStream);
                text = streamWriter.ReadToEnd();
            }

            return text;
        }

        /// <summary>
        /// Computes the SHA512 hash value of the specified string and returns the result as a Base64-encoded string.
        /// </summary>
        /// <param name="text">The input string to compute the hash value for.</param>
        /// <returns>A Base64-encoded string that represents the SHA512 hash value of the input string.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the input string is null.</exception>
        public static string ToSHA512(this string text)
        {
            ArgumentException.ThrowIfNullOrEmpty(text);

            var byteValue = Encoding.UTF8.GetBytes(text);
            var byteHash = SHA512.HashData(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
