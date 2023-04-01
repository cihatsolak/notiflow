namespace Puzzle.Lib.HealthCheck.Infrastructure.Settings
{
    /// <summary>
    /// Represents a setting that contains a list of URL groups to be used in health checks.
    /// </summary>
    internal sealed record HealthChecksUrlGroupSetting
    {
        /// <summary>
        /// Gets or sets the list of URL groups to be used in health checks.
        /// </summary>
        public required List<UrlGroupInformation> UrlGroups { get; init; }
    }

    /// <summary>
    /// Represents a URL group information that is used in health checks.
    /// </summary>
    internal sealed record UrlGroupInformation
    {
        /// <summary>
        /// Gets or sets the service URI for the URL group.
        /// </summary>
        public required Uri ServiceUri { get; init; }

        /// <summary>
        /// Gets or sets the name of the URL group.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets or sets the array of tags for the URL group.
        /// </summary>
        public string[] Tags { get; init; }
    }
}
