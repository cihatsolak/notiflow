namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for URL manipulation.
/// </summary>
public static class UrlExtensions
{
    /// <summary>
    /// Converts a string to a slug URL format.
    /// </summary>
    /// <param name="text">The string to convert to a slug URL format.</param>
    /// <returns>The string in a slug URL format.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="text"/> parameter is null or empty.</exception>
    public static string ToSlugUrl(this string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);

        return PrepareUrl(text);
    }

    private static string PrepareUrl(string text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);

        text = text.ToLowerInvariant();

        var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
        text = Encoding.ASCII.GetString(bytes);
        text = Regex.Replace(text, @"\s", "-", RegexOptions.Compiled);
        text = Regex.Replace(text, @"[^a-z0-9\s-_]", "", RegexOptions.Compiled);
        text = text.Trim('-', '_');
        text = Regex.Replace(text, @"([-_]){2,}", "$1", RegexOptions.Compiled);

        return text;
    }
}
