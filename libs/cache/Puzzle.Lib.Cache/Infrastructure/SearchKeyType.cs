namespace Puzzle.Lib.Cache.Infrastructure;

/// <summary>
/// Represents the types of search keys.
/// </summary>
public enum SearchKeyType : byte
{
    /// <summary>
    /// Specifies that the search key should end with the given search term.
    /// </summary>
    EndsWith = 1,

    /// <summary>
    /// Specifies that the search key should start with the given search term.
    /// </summary>
    StartsWith = 2,

    /// <summary>
    /// Specifies that the search key should contain the given search term.
    /// </summary>
    Include = 3
}
