﻿namespace Puzzle.Lib.Documentation.Infrastructure.Settings
{
    /// <summary>
    /// Represents the configuration settings for Swagger documentation.
    /// </summary>
    public sealed record SwaggerSetting
    {
        /// <summary>
        /// Gets or initializes the name of the Swagger definition.
        /// </summary>
        public required string DefinitionName { get; init; }

        /// <summary>
        /// Gets or initializes the title of the Swagger documentation.
        /// </summary>
        public required string Title { get; init; }

        /// <summary>
        /// Gets or initializes the description of the Swagger documentation.
        /// </summary>
        public required string Description { get; init; }

        /// <summary>
        /// Gets or initializes the version of the Swagger documentation.
        /// </summary>
        public required string Version { get; init; }

        /// <summary>
        /// Gets or initializes the terms of service for the Swagger documentation.
        /// </summary>
        public required Uri TermsOfService { get; init; }

        /// <summary>
        /// Gets or initializes the name of the contact for the Swagger documentation.
        /// </summary>
        public required string ContactName { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the contact for the Swagger documentation.
        /// </summary>
        public required string ContactUrl { get; init; }

        /// <summary>
        /// Gets or initializes the email address of the contact for the Swagger documentation.
        /// </summary>
        public required string ContactEmail { get; init; }

        /// <summary>
        /// Gets or initializes the name of the license for the Swagger documentation.
        /// </summary>
        public required string LicenseName { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the license for the Swagger documentation.
        /// </summary>
        public required string LicenseUrl { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the logo for the Swagger documentation.
        /// </summary>
        public required string LogoUrl { get; init; }
    }
}