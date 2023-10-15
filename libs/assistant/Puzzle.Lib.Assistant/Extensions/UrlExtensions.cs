namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for URL manipulation.
/// </summary>
public static partial class UrlExtensions
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

        text = text.ToLowerInvariant();

        var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(text);
        text = Encoding.ASCII.GetString(bytes);
        text = SpaceToDashConverter().Replace(text, "-");
        text = NonAlphanumericRemover().Replace(text, "");
        text = text.Trim('-', '_');
        text = ConsecutiveSymbolReducer().Replace(text, "$1");

        return text;
    }
}

public static partial class UrlExtensions
{
    [GeneratedRegex("\\s", RegexOptions.Compiled)]
    private static partial Regex SpaceToDashConverter();

    [GeneratedRegex("[^a-z0-9\\s-_]", RegexOptions.Compiled)]
    private static partial Regex NonAlphanumericRemover();

    [GeneratedRegex("([-_]){2,}", RegexOptions.Compiled)]
    private static partial Regex ConsecutiveSymbolReducer();
}
