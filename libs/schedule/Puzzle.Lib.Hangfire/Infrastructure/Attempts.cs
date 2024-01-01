namespace Puzzle.Lib.Hangfire.Infrastructure;

/// <summary>
/// Provides constants for the number of attempts to perform an operation.
/// </summary>
public static class Attempts
{
    /// <summary>
    /// Represents one attempt to perform an operation.
    /// </summary>
    public const int TryOne = 1;

    /// <summary>
    /// Represents two attempts to perform an operation.
    /// </summary>
    public const int TryTwice = 2;

    /// <summary>
    /// Represents three attempts to perform an operation.
    /// </summary>
    public const int TryThreeTimes = 3;

    /// <summary>
    /// Represents four attempts to perform an operation.
    /// </summary>
    public const int TryFourTimes = 4;

    /// <summary>
    /// Represents five attempts to perform an operation.
    /// </summary>
    public const int TryFiveTimes = 5;
}
