namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for input related operations.
/// </summary>
public static partial class InputExtensions
{
    /// <summary>
    /// Removes unnecessary characters from the given phone number string and returns the cleaned version.
    /// </summary>
    /// <param name="phoneNumber">The phone number string to be cleaned.</param>
    /// <returns>The cleaned phone number string without unnecessary characters.</returns>
    /// <exception cref="ArgumentException">Thrown when the given phone number string is null or empty.</exception>
    public static string ToCleanPhoneNumber(this string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            return phoneNumber;

        return NonNumericRemoverRegex().Replace(phoneNumber, string.Empty);
    }

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
            return text;

        return text.Replace(" ", string.Empty).Trim();
    }

    /// <summary>
    /// Converts the given HTML text into plain text by removing all HTML tags.
    /// </summary>
    /// <param name="htmlText">The HTML text to convert.</param>
    /// <returns>The plain text version of the HTML text.</returns>
    public static string ConvertHtmlToText(this string htmlText)
    {
        if (string.IsNullOrWhiteSpace(htmlText))
            return htmlText;

        return CleanHtmlRegex().Replace(htmlText, string.Empty);
    }

    /// <summary>
    /// Checks if the input string contains HTML tags.
    /// </summary>
    /// <param name="text">The input string to check for HTML tags.</param>
    /// <returns>True if HTML tags are found in the input string; otherwise, false.</returns>
    public static bool IsHtml(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return default;

        return CreateHtmlTagRegex().IsMatch(text);
    }
}

public static partial class InputExtensions
{
    [GeneratedRegex("[^\\d]")]
    private static partial Regex NonNumericRemoverRegex();

    [GeneratedRegex("<[^>]*>", RegexOptions.Compiled)]
    private static partial Regex CleanHtmlRegex();

    /// <summary>
    /// Creates and returns a regular expression pattern for matching HTML tags in a text.
    /// </summary>
    /// <returns>A regular expression pattern for HTML tags.</returns>
    [GeneratedRegex("<\\s*([^ >]+)[^>]*>.*?<\\s*/\\s*\\1\\s*>")]
    private static partial Regex CreateHtmlTagRegex();
}