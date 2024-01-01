namespace Puzzle.Lib.Hangfire.Infrastructure;

/// <summary>
/// This class contains commonly used cron expressions for Hangfire recurring jobs.
/// </summary>
public static class CronExpressions
{
    /// <summary>
    /// Cron expression for running a job every day at 3 AM.
    /// </summary>
    public const string At3AM = "00 03 * * *";

    /// <summary>
    /// Cron expression for running a job every day at 4 AM.
    /// </summary>
    public const string At4AM = "00 04 * * *";

    /// <summary>
    /// Cron expression for running a job every 15th minute.
    /// </summary>
    public const string AtEvery15thMinute = "*/15 * * * *";

    /// <summary>
    /// Cron expression for running a job every 10th minute.
    /// </summary>
    public const string AtEvery10thMinute = "*/10 * * * *";

    /// <summary>
    /// Cron expression for running a job every hour.
    /// </summary>
    public const string AtMinuteOPastEveryHour = "0 */1 * * *";

    /// <summary>
    /// Generates a cron expression for running a job at the specified hour and minute every day.
    /// </summary>
    /// <param name="hour">The hour of the day to run the job.</param>
    /// <param name="minute">The minute of the hour to run the job. Defaults to 0.</param>
    /// <returns>The generated cron expression.</returns>
    public static string AtHour(int hour, int minute = 0) => $"{minute} {hour} * * *";

    /// <summary>
    /// Generates a cron expression for running a job once every specified minute.
    /// </summary>
    /// <param name="minute">The minute of the hour to run the job.</param>
    /// <returns>The generated cron expression.</returns>
    public static string OnceMinute(int minute) => $"*/{minute} * * * *";
}
