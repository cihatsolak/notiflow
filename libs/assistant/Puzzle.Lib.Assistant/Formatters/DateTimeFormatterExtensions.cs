namespace Puzzle.Lib.Assistant.Formatters
{
    /// <summary>
    /// Provides extension methods to format date and time values to string representations using custom formats.
    /// </summary>
    public static class DateTimeFormatterExtensions
    {
        /// <summary>
        /// Converts the given DateTime value to a formatted string representation of the date and time in "dd/MM/yyyy HH:mm" format.
        /// </summary>
        /// <param name="dateTime">The DateTime value to be formatted.</param>
        /// <returns>A string representation of the date and time in "dd/MM/yyyy HH:mm" format.</returns>
        public static string ToDateTimeFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Converts the given DateTime value to a formatted string representation of the date in "dd/MM/yyyy" format.
        /// </summary>
        /// <param name="dateTime">The DateTime value to be formatted.</param>
        /// <returns>A string representation of the date in "dd/MM/yyyy" format.</returns>
        public static string ToDateFormat(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Converts the DateTime value to a string representation of the hour and minute in 24-hour format (e.g. "15:30").
        /// </summary>
        /// <param name="dateTime">The DateTime value to convert.</param>
        /// <returns>A string representation of the hour and minute in 24-hour format.</returns>
        public static string ToHourFormat(this DateTime dateTime)
        {
            return dateTime.ToString("HH:mm");
        }
    }
}
