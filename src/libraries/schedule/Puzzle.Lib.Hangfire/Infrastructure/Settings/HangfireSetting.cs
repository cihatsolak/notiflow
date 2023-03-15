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
        public string ConnectionString { get; init; }

        /// <summary>
        /// Gets or sets the expiration timeout in days for a job.
        /// </summary>
        public int JobExpirationTimeoutDay { get; init; }

        /// <summary>
        /// Gets or sets the interval in minutes to poll queues for jobs.
        /// </summary>
        public int QueuePollIntervalMinute { get; init; }

        /// <summary>
        /// Gets or sets the maximum timeout in minutes for a command batch.
        /// </summary>
        public int CommandBatchMaxTimeoutMinute { get; init; }

        /// <summary>
        /// Gets or sets the number of attempts to automatically retry a failed job.
        /// </summary>
        public int GlobalAutomaticRetryAttempts { get; init; }

        /// <summary>
        /// Gets or sets the relative path of the Hangfire dashboard.
        /// </summary>
        public string DashboardPath { get; init; }

        /// <summary>
        /// Gets or sets the title of the Hangfire dashboard.
        /// </summary>
        public string DashboardTitle { get; init; }

        /// <summary>
        /// Gets or sets the relative path of the back button on the dashboard.
        /// </summary>
        public string BackButtonPath { get; init; }

        /// <summary>
        /// Gets or sets the username for Hangfire dashboard authorization.
        /// </summary>
        public string Username { get; init; }

        /// <summary>
        /// Gets or sets the password for Hangfire dashboard authorization.
        /// </summary>
        public string Password { get; init; }
    }
}
