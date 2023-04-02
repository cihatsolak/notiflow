namespace Puzzle.Lib.Security.Infrastructure.Settings
{
    /// <summary>
    /// Represents a record that contains the settings related to hosting.
    /// </summary>
    public sealed record HostingSetting
    {
        /// <summary>
        /// Gets or sets the name of the HTTP header that contains the client's IP address when forwarded by proxy servers.
        /// </summary>
        public required string ForwardedHttpHeader { get; init; }
    }
}
