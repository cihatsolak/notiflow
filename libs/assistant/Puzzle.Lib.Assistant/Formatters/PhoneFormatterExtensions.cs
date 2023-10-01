namespace Puzzle.Lib.Assistant.Formatters;

/// <summary>
/// Provides extension methods for formatting phone numbers.
/// </summary>
public static class PhoneFormatterExtensions
{
    /// <summary>
    /// Formats the phone number in mobile phone format.
    /// </summary>
    /// <param name="mobilePhone">The phone number to format.</param>
    /// <returns>The phone number formatted in GSM format. {0:0 ### ### ## ##}</returns>
    public static string ToMobilePhoneFormat(this string mobilePhone)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobilePhone);

        return string.Format("{0:0 ### ### ## ##}", long.Parse(mobilePhone));
    }
}
