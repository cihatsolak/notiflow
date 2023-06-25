namespace Puzzle.Lib.Security.Services.Protectors;

/// <summary>
/// Interface for a service that provides encryption and decryption functionality for sensitive data.
/// </summary>
public interface IProtectorService
{
    /// <summary>
    /// Encrypts the provided plaintext using a secure encryption algorithm and returns the resulting cipher text as a string.
    /// </summary>
    /// <typeparam name="TData">The type of data to encrypt.</typeparam>
    /// <param name="flatData">The flatdata to encrypt.</param>
    /// <returns>The encrypted cipher text as a string.</returns>
    string Encrypt<TData>(TData flatData);

    /// <summary>
    /// Decrypts the provided cipher text using the appropriate decryption algorithm and returns the original plaintext data.
    /// </summary>
    /// <typeparam name="TData">The type of data to decrypt.</typeparam>
    /// <param name="cipherText">The cipher text data to decrypt.</param>
    /// <returns>The decrypted plaintext data.</returns>
    TData Decrypt<TData>(string cipherText);

    /// <summary>
    /// Encrypts the provided plaintext using a secure encryption algorithm that takes into account a specified time duration for increased security, and returns the resulting cipher text as a string.
    /// </summary>
    /// <typeparam name="TData">The type of data to encrypt.</typeparam>
    /// <param name="flatData">The flatdata to encrypt.</param>
    /// <param name="minutesToExpire">The number of minutes for which the encryption should be time-dependent.</param>
    /// <returns>The encrypted cipher text as a string.</returns>
    string TimeDependentEncrypt<TData>(TData flatData, int minutesToExpire);

    /// <summary>
    /// Decrypts the provided cipher text using the appropriate decryption algorithm that takes into account a specified time duration for increased security, and returns the original plaintext data.
    /// </summary>
    /// <typeparam name="TData">The type of data to decrypt.</typeparam>
    /// <param name="cipherText">The cipher text data to decrypt.</param>
    /// <returns>The decrypted plaintext data.</returns>
    TData TimeDependentDecrypt<TData>(string cipherText);
}