namespace Puzzle.Lib.Localize.Infrastructure;

internal sealed record SupportedCulturesResponse
{
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
}
