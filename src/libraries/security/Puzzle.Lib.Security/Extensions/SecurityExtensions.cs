namespace Puzzle.Lib.Security.Extensions
{
    public static class SecurityExtensions
    {
        public static string CreatePasswordHash(this string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(password);

            return Crypto.HashPassword(password);
        }

        public static bool VerifyPasswordHash(this string hashedPassword, string password)
        {
            ArgumentException.ThrowIfNullOrEmpty(hashedPassword);
            ArgumentException.ThrowIfNullOrEmpty(password);

            return Crypto.VerifyHashedPassword(hashedPassword, password);
        }

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

        public static string ToSHA512(this string text)
        {
            var byteValue = Encoding.UTF8.GetBytes(text);
            var byteHash = SHA512.HashData(byteValue);
            return Convert.ToBase64String(byteHash);
        }
    }
}
