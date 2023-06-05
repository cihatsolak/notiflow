namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Contains extension methods for string manipulation.
/// </summary>
public static class TextExtensions
{
    /// <summary>
    /// Converts the first character of each word in the input string to uppercase and the rest to lowercase, using the rules of the current culture.
    /// </summary>
    /// <param name="text">The input string to convert.</param>
    /// <returns>A new string with each word capitalized.</returns>
    public static string ToTitleCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text);
    }

    /// <summary>
    /// Removes all spaces from the input string and returns the resulting string.
    /// </summary>
    /// <param name="text">The input string to modify.</param>
    /// <returns>A new string with all spaces removed.</returns>
    public static string ToClearSpaces(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return default;

        return text.Replace(" ", string.Empty).Trim();
    }
}
