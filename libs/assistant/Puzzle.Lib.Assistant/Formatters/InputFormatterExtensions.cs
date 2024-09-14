namespace Puzzle.Lib.Assistant.Formatters;

/// <summary>
/// Provides extension methods to format date and time values to string representations using custom formats.
/// </summary>
public static class InputFormatterExtensions
{
    /// <summary>
    /// Converts the given DateTime value to a formatted string representation of the date and time in "dd/MM/yyyy HH:mm" format.
    /// </summary>
    /// <param name="dateTime">The DateTime value to be formatted.</param>
    /// <returns>A string representation of the date and time in "dd/MM/yyyy HH:mm" format.</returns>
    public static string ToDateTimeFormat(this DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the given DateTime value to a formatted string representation of the date in "dd/MM/yyyy" format.
    /// </summary>
    /// <param name="dateTime">The DateTime value to be formatted.</param>
    /// <returns>A string representation of the date in "dd/MM/yyyy" format.</returns>
    public static string ToDateFormat(this DateTime dateTime)
    {
        return dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Converts the DateTime value to a string representation of the hour and minute in 24-hour format (e.g. "15:30").
    /// </summary>
    /// <param name="dateTime">The DateTime value to convert.</param>
    /// <returns>A string representation of the hour and minute in 24-hour format.</returns>
    public static string ToHourFormat(this DateTime dateTime)
    {
        return dateTime.ToString("HH:mm", CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Formats the phone number in mobile phone format.
    /// </summary>
    /// <param name="mobilePhoneNumber">The phone number to format.</param>
    /// <returns>The phone number formatted in GSM format. {0:0 ### ### ## ##}</returns>
    public static string ToMobilePhoneNumberFormat(this string mobilePhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(mobilePhoneNumber))
            return mobilePhoneNumber;

        if (!long.TryParse(mobilePhoneNumber, out long parsedMobilePhoneNumber))
            return mobilePhoneNumber;

        return string.Format(CultureInfo.InvariantCulture, "{0:0 ### ### ## ##}", parsedMobilePhoneNumber);
    }
}
