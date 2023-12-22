namespace Puzzle.Lib.Security.Services.AesCiphers;

/// <summary>
/// Defines an interface for encryption and decryption services.
/// </summary>
public interface IAesCipherService
{
    /// <summary>
    /// Encrypts the provided flat data using a default encryption method.
    /// </summary>
    /// <param name="flatData">The data to be encrypted.</param>
    /// <returns>The encrypted ciphertext.</returns>
    string Encrypt(string flatData);

    /// <summary>
    /// Encrypts the provided flat data using a specified encryption key.
    /// </summary>
    /// <param name="flatData">The data to be encrypted.</param>
    /// <param name="key">The encryption key.</param>
    /// <returns>The encrypted ciphertext.</returns>
    string Encrypt(string flatData, string key);

    /// <summary>
    /// Decrypts the provided ciphertext using a default decryption method.
    /// </summary>
    /// <param name="cipherText">The ciphertext to be decrypted.</param>
    /// <returns>The decrypted flat data.</returns>
    string Decrypt(string cipherText);

    /// <summary>
    /// Decrypts the provided ciphertext using a specified decryption key.
    /// </summary>
    /// <param name="cipherText">The ciphertext to be decrypted.</param>
    /// <param name="key">The decryption key.</param>
    /// <returns>The decrypted flat data.</returns>
    string Decrypt(string cipherText, string key);
}
