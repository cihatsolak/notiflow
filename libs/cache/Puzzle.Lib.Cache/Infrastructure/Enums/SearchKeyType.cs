namespace Puzzle.Lib.Cache.Infrastructure.Enums;

/// <summary>
/// Represents the types of search keys.
/// </summary>
public enum SearchKeyType : byte
{
    /// <summary>
    /// Specifies that the search key should end with the given search term.
    /// </summary>
    [Description("Ends With")]
    EndsWith = 1,

    /// <summary>
    /// Specifies that the search key should start with the given search term.
    /// </summary>
    [Description("Starts With")]
    StartsWith = 2,

    /// <summary>
    /// Specifies that the search key should contain the given search term.
    /// </summary>
    [Description("Contains")]
    Include = 3
}
