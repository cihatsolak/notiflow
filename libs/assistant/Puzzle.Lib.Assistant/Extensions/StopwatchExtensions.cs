namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for the Stopwatch class to retrieve the elapsed time in a formatted string.
/// </summary>
public static class StopwatchExtensions
{
    /// <summary>
    /// Gets the elapsed time of the Stopwatch instance in a formatted string (hh:mm:ss.ff).
    /// </summary>
    /// <param name="stopwatch">The Stopwatch instance.</param>
    /// <returns>A formatted string representing the elapsed time.</returns>
    public static string GetElapsedTime(this Stopwatch stopwatch)
    {
        TimeSpan elapsedTime = stopwatch.Elapsed;

        return string.Format("{0:00}:{1:00}:{2:00}.{3:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds, elapsedTime.Milliseconds / 10);
    }
}
