namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for collections.
/// </summary>
public static class CollectionExtensions
{
    /// <summary>
    /// Determines if the given enumerable is null or does not contain any elements.
    /// </summary>
    /// <typeparam name="T">The type of the enumerable.</typeparam>
    /// <param name="source">The enumerable to check.</param>
    /// <returns>True if the enumerable is null or does not contain any elements, otherwise false.</returns>
    public static bool IsNullOrNotAny<T>(this IEnumerable<T> source)
    {
        return !(source?.Any() ?? false);
    }

    /// <summary>
    /// Returns the original enumerable if it's not null, otherwise returns an empty enumerable.
    /// </summary>
    /// <typeparam name="T">The type of the enumerable.</typeparam>
    /// <param name="source">The enumerable to check.</param>
    /// <returns>The original enumerable if it's not null, otherwise an empty enumerable.</returns>
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
    {
        return source ?? [];
    }

    /// <summary>
    /// Joins the elements of an IEnumerable<string> into a single string with a specified separator.
    /// </summary>
    /// <param name="source">The IEnumerable<string> to join.</param>
    /// <param name="separator">The character used to separate the joined elements.</param>
    /// <returns>A string containing the joined elements separated by the specified separator.</returns>
    public static string JoinWithSeparator(this List<string> source, char separator)
    {
        if (source.IsNullOrNotAny())
        {
            return string.Empty;
        }

        return string.Join(separator, source);
    }

    /// <summary>
    /// Splits the input string into an array of substrings based on the specified separator. 
    /// Empty entries are removed, and each entry is trimmed of any leading or trailing whitespace.
    /// </summary>
    /// <param name="input">The input string to be split.</param>
    /// <param name="separator">The character used as a separator for splitting the input string.</param>
    /// <returns>An array of strings that are the result of splitting the input string by the specified separator. 
    /// If the input string is null, whitespace, or empty, an empty array is returned.</returns>
    public static string[] SplitBySeparator(string input, char separator)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return [];
        }

        return input.Split(separator, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
