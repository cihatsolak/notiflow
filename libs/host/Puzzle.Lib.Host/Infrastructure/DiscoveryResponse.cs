namespace Puzzle.Lib.Host.Infrastructure;

public sealed record DiscoveryResponse(
    DateTime UpdateTime,
    DateTime UpdateTimeUtc,
    string MachineName,
    string OsVersion,
    string Framework);