namespace Puzzle.Lib.Security.Infrastructure;

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
