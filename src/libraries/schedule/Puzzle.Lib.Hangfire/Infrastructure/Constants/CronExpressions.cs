namespace Puzzle.Lib.Hangfire.Infrastructure.Constants
{
    /// <summary>
    /// The working times of the jobs added
    /// </summary>
    public static class CronExpressions
    {
        /// <summary>
        /// Every morning at 03:00
        /// </summary>
        public const string At3AM = "00 03 * * *";

        /// <summary>
        /// Every morning at 04:00
        /// </summary>
        public const string At4AM = "00 04 * * *";

        /// <summary>
        /// Once every 15 minutes
        /// </summary>
        public const string AtEvery15thMinute = "*/15 * * * *";

        /// <summary>
        /// 1 time every 10 minutes
        /// </summary>
        public const string AtEvery10thMinute = "*/10 * * * *";

        /// <summary>
        /// 1 time per hour
        /// </summary>
        public const string AtMinuteOPastEveryHour = "0 */1 * * *";

        /// <summary>
        /// Will work at the relevant hour
        /// </summary>
        /// <param name="hour">What time?</param>
        /// <remarks>it will only work 1 times at the specified time.</remarks>
        /// <returns>cron expression</returns>
        public static string AtHour(int hour, int minute = 0) => $"{minute} {hour} * * *";

        /// <summary>
        /// Will work at the relevant minute
        /// </summary>
        /// <param name="minute">What minute?</param>
        /// <returns>cron expression</returns>
        public static string OnceMinute(int minute) => $"*/{minute} * * * *";
    }
}
