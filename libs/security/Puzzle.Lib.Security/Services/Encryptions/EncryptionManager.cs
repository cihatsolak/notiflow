namespace Puzzle.Lib.Security.Services.Encryptions;

internal sealed class EncryptionManager : IEncryptionService
{
    private readonly EncryptionSetting _encryptionSetting;

    public EncryptionManager(IOptions<EncryptionSetting> encryptionSetting)
    {
        _encryptionSetting = encryptionSetting.Value;
    }

    public string Encrypt(string flatData)
    {
        ArgumentException.ThrowIfNullOrEmpty(flatData);

        byte[] rgbKey = Encoding.UTF8.GetBytes(_encryptionSetting.Key);

        using Aes aes = Aes.Create();
        using ICryptoTransform cryptoTransform = aes.CreateEncryptor(rgbKey, aes.IV);
        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write);
        using (StreamWriter streamWriter = new(cryptoStream))
        {
            streamWriter.Write(flatData);
        }

        byte[] iv = aes.IV;
        byte[] decryptedContent = memoryStream.ToArray();
        byte[] result = new byte[iv.Length + decryptedContent.Length];

        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

        return Convert.ToBase64String(result);
    }

    public string Encrypt(string flatData, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(flatData);
        ArgumentException.ThrowIfNullOrEmpty(key);

        byte[] rgbKey = Encoding.UTF8.GetBytes(key);

        using Aes aes = Aes.Create();
        using ICryptoTransform cryptoTransform = aes.CreateEncryptor(rgbKey, aes.IV);
        using MemoryStream memoryStream = new();
        using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Write);
        using (StreamWriter streamWriter = new(cryptoStream))
        {
            streamWriter.Write(flatData);
        }

        byte[] iv = aes.IV;
        byte[] decryptedContent = memoryStream.ToArray();
        byte[] result = new byte[iv.Length + decryptedContent.Length];

        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string cipherText)
    {
        ArgumentException.ThrowIfNullOrEmpty(cipherText);

        byte[] fullCipher = Convert.FromBase64String(cipherText);
        byte[] iv = new byte[16];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

        byte[] rgbKey = Encoding.UTF8.GetBytes(_encryptionSetting.Key);

        using Aes aes = Aes.Create();
        using ICryptoTransform cryptoTransform = aes.CreateDecryptor(rgbKey, iv);

        string flatData;
        using (MemoryStream memoryStream = new(cipher))
        {
            using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader streamWriter = new(cryptoStream);
            flatData = streamWriter.ReadToEnd();
        }

        return flatData;
    }

    public string Decrypt(string cipherText, string key)
    {
        ArgumentException.ThrowIfNullOrEmpty(cipherText);
        ArgumentException.ThrowIfNullOrEmpty(key);

        byte[] fullCipher = Convert.FromBase64String(cipherText);
        byte[] iv = new byte[16];
        byte[] cipher = new byte[fullCipher.Length - iv.Length];

        Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
        Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

        byte[] rgbKey = Encoding.UTF8.GetBytes(key);

        using Aes aes = Aes.Create();
        using ICryptoTransform cryptoTransform = aes.CreateDecryptor(rgbKey, iv);

        string flatData;
        using (MemoryStream memoryStream = new(cipher))
        {
            using CryptoStream cryptoStream = new(memoryStream, cryptoTransform, CryptoStreamMode.Read);
            using StreamReader streamWriter = new(cryptoStream);
            flatData = streamWriter.ReadToEnd();
        }

        return flatData;
    }
}
