﻿namespace Puzzle.Lib.Version.Infrastructure.Settings
{
    /// <summary>
    /// Represents the API version settings.
    /// </summary>
    public sealed record ApiVersionSetting
    {
        /// <summary>
        /// Gets or sets the name of the header used to specify the API version.
        /// </summary>
        public required string HeaderName { get; init; }

        /// <summary>
        /// Gets or sets the major version number of the API.
        /// </summary>
        public required int MajorVersion { get; init; }

        /// <summary>
        /// Gets or sets the minor version number of the API.
        /// </summary>
        public required int MinorVersion { get; init; }
    }
}