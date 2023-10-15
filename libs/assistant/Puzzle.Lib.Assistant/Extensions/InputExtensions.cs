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
}

public static partial class InputExtensions
{
    [GeneratedRegex("[^\\d]")]
    private static partial Regex NonNumericRemoverRegex();
}