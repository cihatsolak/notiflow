namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for input related operations.
/// </summary>
public static partial class InputExtensions
{
    /// <summary>
    /// Removes unnecessary characters from the given phone number string and returns the cleaned version.
    /// </summary>
    /// <param name="mobilePhoneNumber">The phone number string to be cleaned.</param>
    /// <returns>The cleaned phone number string without unnecessary characters.</returns>
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static string ToMobilePhoneNumber(this string mobilePhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(mobilePhoneNumber))
            return mobilePhoneNumber;

        return NonNumericRemoverRegex().Replace(mobilePhoneNumber, string.Empty);
    }

    /// <summary>
    /// Converts the first character of each word in the input string to uppercase and the rest to lowercase, using the rules of the current culture.
    /// </summary>
    /// <param name="text">The input string to convert.</param>
    /// <returns>A new string with each word capitalized.</returns>
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
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
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
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
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static string ToCleanTextFromHtml(this string htmlText)
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
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static bool IsHtml(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return default;

        return CreateHtmlTagRegex().IsMatch(text);
    }

    /// <summary>
    /// Calculates the price with VAT included for the given price and VAT rate.
    /// </summary>
    /// <param name="price">The original price to calculate the new price from.</param>
    /// <param name="vatRate">The VAT rate to apply to the price.</param>
    /// <returns>The new price with VAT included, rounded up to the nearest integer.</returns>
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static int ApplyVat(this decimal price, int vatRate)
    {
        if (Math.Sign(price) != 1 || Math.Sign(vatRate) != 1)
            return default;

        return (int)Math.Ceiling(price + price * vatRate / 100);
    }

    /// <summary>
    /// Calculates the discount rate for the given main price and sales price.
    /// </summary>
    /// <param name="mainPrice">The original price to calculate the discount from.</param>
    /// <param name="salesPrice">The discounted price.</param>
    /// <returns>The discount rate as a percentage, rounded to two decimal places.</returns>
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static int FindDiscountPercentage(this decimal mainPrice, decimal salesPrice)
    {
        if (Math.Sign(mainPrice) != 1 || Math.Sign(salesPrice) != 1)
            return default;

        decimal discountRate = Math.Round((mainPrice - salesPrice) / mainPrice * 100, 2);
        if (default(int) > discountRate)
            return default;

        return (int)discountRate;
    }

    /// <summary>
    /// Converts a string to a slug URL format.
    /// </summary>
    /// <param name="text">The string to convert to a slug URL format.</param>
    /// <returns>The string in a slug URL format.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="text"/> parameter is null or empty.</exception>
    /// <exception cref="RegexMatchTimeoutException"> Thrown when a regular expression matching operation exceeds the specified timeout.</exception>
    public static string ToSlugUrl(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

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

public static partial class InputExtensions
{
    /// <summary>
    /// Gets a regular expression for removing HTML tags from a text.
    /// </summary>
    /// <remarks>
    /// This regular expression is designed to match and remove HTML tags from a given text.
    /// </remarks>
    /// <returns>A regular expression for removing HTML tags.</returns>
    [GeneratedRegex("<[^>]*>", RegexOptions.Compiled, 2000)]
    private static partial Regex CleanHtmlRegex();

    /// <summary>
    /// Gets a regular expression for removing non-numeric characters from a text.
    /// </summary>
    /// <remarks>
    /// This regular expression is designed to match and remove characters that are not numeric from a given text.
    /// </remarks>
    /// <returns>A regular expression for removing non-numeric characters.</returns>
    [GeneratedRegex("[^\\d]", RegexOptions.Compiled, 2000)]
    private static partial Regex NonNumericRemoverRegex();

    /// <summary>
    /// Creates and returns a regular expression pattern for matching HTML tags in a text.
    /// </summary>
    /// <returns>A regular expression pattern for HTML tags.</returns>
    [GeneratedRegex("<\\s*([^ >]+)[^>]*>.*?<\\s*/\\s*\\1\\s*>", RegexOptions.Compiled, 2000)]
    private static partial Regex CreateHtmlTagRegex();

    /// <summary>
    /// Regex pattern for converting whitespace characters to dashes.
    /// </summary>
    /// <returns>Regex pattern for converting whitespace characters to dashes.</returns>
    [GeneratedRegex("\\s", RegexOptions.Compiled, 2000)]
    private static partial Regex SpaceToDashConverter();

    /// <summary>
    /// Regex pattern for removing non-alphanumeric characters.
    /// </summary>
    /// <returns>Regex pattern for removing non-alphanumeric characters.</returns>
    [GeneratedRegex("[^a-z0-9\\s-_]", RegexOptions.Compiled, 2000)]
    private static partial Regex NonAlphanumericRemover();

    /// <summary>
    /// Regex pattern for reducing consecutive symbols (hyphens or underscores).
    /// </summary>
    /// <returns>Regex pattern for reducing consecutive symbols (hyphens or underscores).</returns>
    [GeneratedRegex("([-_]){2,}", RegexOptions.Compiled, 2000)]
    private static partial Regex ConsecutiveSymbolReducer();
}