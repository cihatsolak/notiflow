namespace Puzzle.Lib.HealthCheck.Infrastructure.Settings
{
    /// <summary>
    /// Represents the settings for a health check UI, such as the path to the UI and any custom styles to apply.
    /// </summary>
    internal sealed record HealthUISetting
    {
        /// <summary>
        /// Gets or initializes the path to the health check UI.
        /// </summary>
        public string UIPath { get; init; }

        /// <summary>
        /// Gets or sets the path to the health check response.
        /// </summary>
        public string ResponsePath { get; set; }

        /// <summary>
        /// Gets or initializes the path to the custom CSS file for the health check UI.
        /// </summary>
        public string CustomCssPath { get; init; }

        /// <summary>
        /// Gets or sets a value indicating whether custom styles are applied to the health check UI.
        /// </summary>
        public bool IsCustomStyle { get; set; }
    }
}
