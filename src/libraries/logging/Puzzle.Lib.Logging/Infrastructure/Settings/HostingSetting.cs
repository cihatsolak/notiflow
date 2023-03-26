namespace Puzzle.Lib.Logging.Infrastructure.Settings
{
    public sealed record HostingSetting
    {
        public string ForwardedHttpHeader { get; init; }
    }
}
