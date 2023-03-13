namespace Puzzle.Lib.Documentation.Infrastructure.Settings
{
    /// <summary>
    /// Represents the configuration settings for Swagger documentation.
    /// </summary>
    public sealed record SwaggerSetting
    {
        /// <summary>
        /// Gets or initializes the name of the Swagger definition.
        /// </summary>
        public string DefinitionName { get; init; }

        /// <summary>
        /// Gets or initializes the title of the Swagger documentation.
        /// </summary>
        public string Title { get; init; }

        /// <summary>
        /// Gets or initializes the description of the Swagger documentation.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets or initializes the version of the Swagger documentation.
        /// </summary>
        public string Version { get; init; }

        /// <summary>
        /// Gets or initializes the terms of service for the Swagger documentation.
        /// </summary>
        public string TermsOfService { get; init; }

        /// <summary>
        /// Gets or initializes the name of the contact for the Swagger documentation.
        /// </summary>
        public string ContactName { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the contact for the Swagger documentation.
        /// </summary>
        public string ContactUrl { get; init; }

        /// <summary>
        /// Gets or initializes the email address of the contact for the Swagger documentation.
        /// </summary>
        public string ContactEmail { get; init; }

        /// <summary>
        /// Gets or initializes the name of the license for the Swagger documentation.
        /// </summary>
        public string LicenseName { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the license for the Swagger documentation.
        /// </summary>
        public string LicenseUrl { get; init; }

        /// <summary>
        /// Gets or initializes the URL of the logo for the Swagger documentation.
        /// </summary>
        public string LogoUrl { get; init; }

        /// <summary>
        /// Gets or initializes a value indicating whether the Swagger documentation has a closed schema.
        /// </summary>
        public bool IsClosedSchema { get; init; }

        /// <summary>
        /// Gets or initializes a value indicating whether the Swagger documentation has a basic security scheme.
        /// </summary>
        public bool IsHaveBasicSecurityScheme { get; init; }

        /// <summary>
        /// Gets or initializes a value indicating whether the Swagger documentation has a JWT security scheme.
        /// </summary>
        public bool IsHaveJwtSecurityScheme { get; init; }

        /// <summary>
        /// Gets or initializes a value indicating whether the Swagger documentation has default headers.
        /// </summary>
        public bool IsHaveDefaultHeaders { get; init; }
    }
}