namespace Puzzle.Lib.Hangfire.Infrastructure;

/// <summary>
/// Represents the settings required for configuring Hangfire.
/// </summary>
public sealed record HangfireSetting
{
    /// <summary>
    /// Gets or sets the connection string for the storage.
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets the number of attempts to automatically retry a failed job.
    /// </summary>
    public int GlobalAutomaticRetryAttempts { get; set; }

    /// <summary>
    /// Gets or sets the username for Hangfire dashboard authorization.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password for Hangfire dashboard authorization.
    /// </summary>
    public string Password { get; set; }
}
