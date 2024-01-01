namespace Puzzle.Lib.Validation.Infrastructure;

/// <summary>
/// Provides extension methods for email validation.
/// </summary>
internal static class EmailValidateExtension
{
    static readonly string[] TLDS = ["com", "net", "org", "edu", "gov", "us", "uk", "ca", "au", "fr"];

    /// <summary>
    /// Validates the top-level domain (TLD) of an email address.
    /// </summary>
    /// <remarks>
    /// This method checks whether the TLD of an email address is valid by comparing it with a list of known TLDs.
    /// </remarks>
    /// <param name="email">The email address to validate.</param>
    /// <returns>A boolean value indicating whether the TLD of the email address is valid.</returns>
    internal static bool ValidateTld(string email)
    {
        string[] emailParts = email.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        int lastPartIndex = emailParts.Length - 1;

        return Array.Exists(TLDS, tld => tld == emailParts[lastPartIndex]);
    }
}
