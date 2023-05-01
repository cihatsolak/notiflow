namespace Puzzle.Lib.Database.Infrastructure.Settings
{
    /// <summary>
    /// Represents a SQL setting, containing a connection string and a flag indicating whether the query should be split.
    /// </summary>
    internal sealed record SqlSetting
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        [JsonRequired]
        public required string ConnectionString { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the query should be split.
        /// </summary>
        public required bool IsSplitQuery { get; set; }
    }
}
