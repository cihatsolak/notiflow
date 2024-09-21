namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Gets a regular expression for removing HTML tags from a text.
/// </summary>
/// <remarks>
/// This regular expression is designed to match and remove HTML tags from a given text.
/// </remarks>
/// <returns>A regular expression for removing HTML tags.</returns>
public static partial class InputExtensions
{
    /// <summary>
    /// Represents the time-out period in seconds for a specific operation.
    /// </summary>
    private const int MATCH_TIME_OUT_MILLISECONDS = 2000;

    /// <summary>
    /// Gets a regular expression for removing HTML tags from a text.
    /// </summary>
    /// <remarks>
    /// This regular expression is designed to match and remove HTML tags from a given text.
    /// </remarks>
    /// <returns>A regular expression for removing HTML tags.</returns>
    [GeneratedRegex("<[^>]*>", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex CleanHtmlRegex();

    /// <summary>
    /// Gets a regular expression for removing non-numeric characters from a text.
    /// </summary>
    /// <remarks>
    /// This regular expression is designed to match and remove characters that are not numeric from a given text.
    /// </remarks>
    /// <returns>A regular expression for removing non-numeric characters.</returns>
    [GeneratedRegex("[^\\d]", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex NonNumericRemoverRegex();

    /// <summary>
    /// Creates and returns a regular expression pattern for matching HTML tags in a text.
    /// </summary>
    /// <returns>A regular expression pattern for HTML tags.</returns>
    [GeneratedRegex("<\\s*([^ >]+)[^>]*>.*?<\\s*/\\s*\\1\\s*>", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex CreateHtmlTagRegex();

    /// <summary>
    /// Regex pattern for converting whitespace characters to dashes.
    /// </summary>
    /// <returns>Regex pattern for converting whitespace characters to dashes.</returns>
    [GeneratedRegex("\\s", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex SpaceToDashConverter();

    /// <summary>
    /// Regex pattern for removing non-alphanumeric characters.
    /// </summary>
    /// <returns>Regex pattern for removing non-alphanumeric characters.</returns>
    [GeneratedRegex("[^a-z0-9\\s-_]", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex NonAlphanumericRemover();

    /// <summary>
    /// Regex pattern for reducing consecutive symbols (hyphens or underscores).
    /// </summary>
    /// <returns>Regex pattern for reducing consecutive symbols (hyphens or underscores).</returns>
    [GeneratedRegex("([-_]){2,}", RegexOptions.Compiled, MATCH_TIME_OUT_MILLISECONDS)]
    private static partial Regex ConsecutiveSymbolReducer();
}
