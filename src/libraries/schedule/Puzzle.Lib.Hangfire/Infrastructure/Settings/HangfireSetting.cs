namespace Puzzle.Lib.Hangfire.Infrastructure.Settings
{
    internal interface IHangfireSetting
    {
        string ConnectionString { get; init; }
        int JobExpirationTimeoutDay { get; init; }
        int QueuePollIntervalMinute { get; init; }
        int CommandBatchMaxTimeoutMinute { get; init; }
        int GlobalAutomaticRetryAttempts { get; init; }
        string DashboardPath { get; init; }
        string DashboardTitle { get; init; }
        string BackButtonPath { get; init; }
        string Username { get; init; }
        string Password { get; init; }
    }

    internal sealed record HangfireSetting : IHangfireSetting
    {
        public string ConnectionString { get; init; }
        public int JobExpirationTimeoutDay { get; init; }
        public int QueuePollIntervalMinute { get; init; }
        public int CommandBatchMaxTimeoutMinute { get; init; }
        public int GlobalAutomaticRetryAttempts { get; init; }
        public string DashboardPath { get; init; }
        public string DashboardTitle { get; init; }
        public string BackButtonPath { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
