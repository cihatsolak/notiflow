namespace Puzzle.Lib.Hangfire.Infrastructure.Settings
{
    /// <summary>
    /// Represents the settings required for configuring Hangfire.
    /// </summary>
    internal sealed record HangfireSetting
    {
        /// <summary>
        /// Gets or sets the connection string for the storage.
        /// </summary>
        public required string ConnectionString { get; init; }

        /// <summary>
        /// Gets or sets the expiration timeout in days for a job.
        /// </summary>
        public required int JobExpirationTimeoutDay { get; init; }

        /// <summary>
        /// Gets or sets the interval in minutes to poll queues for jobs.
        /// </summary>
        public required int QueuePollIntervalMinute { get; init; }

        /// <summary>
        /// Gets or sets the maximum timeout in minutes for a command batch.
        /// </summary>
        public required int CommandBatchMaxTimeoutMinute { get; init; }

        /// <summary>
        /// Gets or sets the number of attempts to automatically retry a failed job.
        /// </summary>
        public required int GlobalAutomaticRetryAttempts { get; init; }

        /// <summary>
        /// Gets or sets the relative path of the Hangfire dashboard.
        /// </summary>
        public required string DashboardPath { get; init; }

        /// <summary>
        /// Gets or sets the title of the Hangfire dashboard.
        /// </summary>
        public required string DashboardTitle { get; init; }

        /// <summary>
        /// Gets or sets the relative path of the back button on the dashboard.
        /// </summary>
        public required string BackButtonPath { get; init; }

        /// <summary>
        /// Gets or sets the username for Hangfire dashboard authorization.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// Gets or sets the password for Hangfire dashboard authorization.
        /// </summary>
        public required string Password { get; init; }
    }
}
