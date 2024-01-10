namespace Puzzle.Lib.Cache.Infrastructure;

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

/// <summary>
/// Specifies the duration for which an item should be cached in minutes.
/// </summary>
public enum CacheDuration
{
    /// <summary>
    /// Cache item for 1 minute.
    /// </summary>
    OneMinute = 1,

    /// <summary>
    /// Cache item for 2 minutes.
    /// </summary>
    TwoMinutes = 2,

    /// <summary>
    /// Cache item for 5 minutes.
    /// </summary>
    FiveMinutes = 5,

    /// <summary>
    /// Cache item for 10 minutes.
    /// </summary>
    TenMinutes = 10,

    /// <summary>
    /// Cache item for 15 minutes.
    /// </summary>
    FifteenMinutes = 15,

    /// <summary>
    /// Cache item for 30 minutes.
    /// </summary>
    ThirtyMinutes = 30,

    /// <summary>
    /// Cache item for 1 hour (60 minutes).
    /// </summary>
    OneHour = 60,

    /// <summary>
    /// Cache item for 2 hours (120 minutes).
    /// </summary>
    TwoHours = 120,

    /// <summary>
    /// Cache item for 6 hours (360 minutes).
    /// </summary>
    SixHours = 360,

    /// <summary>
    /// Cache item for 1 day (1440 minutes).
    /// </summary>
    OneDay = 1440
}
