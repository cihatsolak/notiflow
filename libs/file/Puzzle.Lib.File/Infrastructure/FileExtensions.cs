namespace Puzzle.Lib.File.Infrastructure;

/// <summary>
/// Provides a collection of extension methods for file-related operations, including cleaning and regulating file names.
/// </summary>
internal static partial class FileExtensions
{
    [GeneratedRegex("[\\\"!'%^&/()=?_ @€¨~,;:.<>|]")]
    private static partial Regex UnwantedCharactersRegex();

    /// <summary>
    /// Cleans and regulates the input string by removing unwanted characters, replacing Turkish characters, and converting spaces to hyphens in a culture-independent manner.
    /// </summary>
    /// <param name="name">The input string to be cleaned and regulated.</param>
    /// <returns>A cleaned and regulated version of the input string.</returns>
    internal static string CharacterRegulatory(string name)
    {
        string cleanName = UnwantedCharactersRegex().Replace(name, string.Empty);
        
        cleanName = cleanName
            .Replace("Ö", "o")
            .Replace("ö", "o")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("ı", "i")
            .Replace("İ", "i")
            .Replace("ğ", "g")
            .Replace("Ğ", "g")
            .Replace("ş", "s")
            .Replace("Ş", "s")
            .Replace("Ç", "c")
            .Replace("ç", "c");

        return cleanName.Replace(" ", "-").ToLowerInvariant();
    }
}