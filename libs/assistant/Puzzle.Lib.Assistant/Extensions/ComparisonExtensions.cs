namespace Puzzle.Lib.Assistant.Extensions
{
    /// <summary>
    /// Provides extension methods for string comparison with ordinal ignore case, ordinal and current culture ignore case options.
    /// </summary>
    public static class ComparisonExtensions
    {
        /// <summary>
        /// Determines whether two specified strings have the same value, ignoring their case and using ordinal culture.
        /// </summary>
        /// <param name="value">The current string to compare.</param>
        /// <param name="valueToCompare">The string to compare to the current string.</param>
        /// <returns>True if the strings are equal, ignoring case; otherwise, false.</returns>
        public static bool OrdinalIgnoreCase(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether two specified strings have the same value, using ordinal culture.
        /// </summary>
        /// <param name="value">The current string to compare.</param>
        /// <param name="valueToCompare">The string to compare to the current string.</param>
        /// <returns>True if the strings are equal; otherwise, false.</returns>
        public static bool Ordinal(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.Ordinal);
        }

        /// <summary>
        /// Determines whether two specified strings have the same value, ignoring their case and using the current culture.
        /// </summary>
        /// <param name="value">The current string to compare.</param>
        /// <param name="valueToCompare">The string to compare to the current string.</param>
        /// <returns>True if the strings are equal, ignoring case; otherwise, false.</returns>
        public static bool CurrentCultureIgnoreCase(this string value, string valueToCompare)
        {
            if (string.IsNullOrWhiteSpace(value) && string.IsNullOrWhiteSpace(valueToCompare))
                return true;

            if (string.IsNullOrWhiteSpace(value))
                return false;

            return value.Equals(valueToCompare, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
