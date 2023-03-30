namespace Puzzle.Lib.Cache.Infrastructure.Extensions
{
    /// <summary>
    /// Contains extension methods for converting data to and from JSON format.
    /// </summary>
    internal static class ConverterExtensions
    {
        /// <summary>
        /// Converts the given value to a JSON string if it's not of type string.
        /// </summary>
        /// <typeparam name="TData">The type of the value to convert.</typeparam>
        /// <param name="value">The value to convert.</param>
        /// <returns>A JSON string representation of the value if it's not of type string, otherwise the original value as a string.</returns>
        internal static string ToJsonIfNotStringType<TData>(this TData value)
        {
            if (typeof(TData) == typeof(string))
                return (string)Convert.ChangeType(value, typeof(string));

            return JsonSerializer.Serialize(value);
        }
    }
}
