namespace Puzzle.Lib.Assistant.Formatters;

/// <summary>
/// Provides extension methods for formatting phone numbers.
/// </summary>
public static class PhoneFormatterExtensions
{
    /// <summary>
    /// Formats the phone number in GSM format.
    /// </summary>
    /// <param name="gsmNumber">The phone number to format.</param>
    /// <returns>The phone number formatted in GSM format. {0:0 ### ### ## ##}</returns>
    public static string ToGsmFormat(this string gsmNumber)
    {
        ArgumentException.ThrowIfNullOrEmpty(gsmNumber);

        return string.Format("{0:0 ### ### ## ##}", long.Parse(gsmNumber));
    }
}
