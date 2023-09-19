namespace Puzzle.Lib.Security.Infrastructure.Settings;

public sealed record RedisProtectorSetting
{
    [JsonRequired]
    public required string ConnectionString { get; init; }
    public required int DatabaseNumber { get; init; }

    [JsonRequired]
    public required string Key { get; init; }

    [JsonRequired]
    public required int ExpirationDays { get; init; }
}
