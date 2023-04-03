namespace Puzzle.Lib.Assistants.Extensions
{
    /// <summary>
    /// Provides extension methods for input related operations.
    /// </summary>
    public static class InputExtensions
    {
        /// <summary>
        /// Removes unnecessary characters from the given phone number string and returns the cleaned version.
        /// </summary>
        /// <param name="phoneNumber">The phone number string to be cleaned.</param>
        /// <returns>The cleaned phone number string without unnecessary characters.</returns>
        /// <exception cref="ArgumentException">Thrown when the given phone number string is null or empty.</exception>
        public static string ToCleanPhoneNumber(this string phoneNumber)
        {
            ArgumentException.ThrowIfNullOrEmpty(phoneNumber);

            return Regex.Replace(phoneNumber, @"[^\d]", "");
        }
    }
}
