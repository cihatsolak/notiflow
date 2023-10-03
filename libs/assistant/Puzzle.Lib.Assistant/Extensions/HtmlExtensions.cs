namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for HTML related operations.
/// </summary>
public static partial class HtmlExtensions
{
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

public static partial class HtmlExtensions
{
    [GeneratedRegex("<[^>]*>")]
    private static partial Regex CleanHtmlRegex();

    /// <summary>
    /// Creates and returns a regular expression pattern for matching HTML tags in a text.
    /// </summary>
    /// <returns>A regular expression pattern for HTML tags.</returns>
    [GeneratedRegex("<\\s*([^ >]+)[^>]*>.*?<\\s*/\\s*\\1\\s*>")]
    private static partial Regex CreateHtmlTagRegex();
}