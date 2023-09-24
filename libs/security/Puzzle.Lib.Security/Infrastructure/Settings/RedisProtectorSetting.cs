namespace Puzzle.Lib.Security.Infrastructure.Settings;

/// <summary>
/// Represents the configuration settings for protecting data in Redis.
/// </summary>
public sealed record RedisProtectorSetting
{
    /// <summary>
    /// Gets or sets the connection string for Redis.
    /// </summary>
    [JsonRequired]
    public required string ConnectionString { get; init; }

    /// <summary>
    /// Gets or sets the number of the Redis database to use.
    /// </summary>
    public required int DatabaseNumber { get; init; }

    /// <summary>
    /// Gets or sets the key used for Redis data storage.
    /// </summary>
    [JsonRequired]
    public required string Key { get; init; }

    /// <summary>
    /// Gets or sets the number of days until Redis data expiration.
    /// </summary>
    [JsonRequired]
    public required int ExpirationDays { get; init; }
}

