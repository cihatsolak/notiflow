namespace Puzzle.Lib.Cache.Infrastructure;

/// <summary>
/// Represents the configuration settings for the Redis server.
/// </summary>
public sealed record RedisServerSetting
{
    /// <summary>
    /// Gets or sets the Redis server connection string.
    /// </summary>
    [JsonRequired]
    public string ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to abort the connection if the server fails to respond during the connection phase.
    /// </summary>
    public bool AbortOnConnectFail { get; set; }

    /// <summary>
    /// Gets or sets the async timeout in seconds.
    /// </summary>
    public int AsyncTimeOutSecond { get; set; }

    /// <summary>
    /// Gets or sets the connection timeout in seconds.
    /// </summary>
    public int ConnectTimeOutSecond { get; set; }

    /// <summary>
    /// Gets or sets the Redis server username.
    /// </summary>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the Redis server password.
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the default database to be used in the Redis server.
    /// </summary>
    public int DefaultDatabase { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the Redis server allows administrative operations.
    /// </summary>
    public bool AllowAdmin { get; set; }

    /// <summary>
    /// Gets or sets the number of times to retry the operation in case of failure.
    /// </summary>
    public int RetryCount { get; set; }
}
