namespace Puzzle.Lib.HealthCheck.Infrastructure.Settings
{
    /// <summary>
    /// Represents a health check setting for a specific endpoint.
    /// </summary>
    internal sealed record HealthSetting
    {
        /// <summary>
        /// Gets or sets the name of the endpoint.
        /// </summary>
        public string Name { get; init; }

        /// <summary>
        /// Gets or sets the URI of the endpoint.
        /// </summary>
        public string Uri { get; init; }

        /// <summary>
        /// Gets or sets the payload to send in the request.
        /// </summary>
        public string Payload { get; init; }

        /// <summary>
        /// Gets or sets the restore payload to send in the request to restore the service.
        /// </summary>
        public string RestorePayload { get; init; }

        /// <summary>
        /// Gets or sets the time interval in seconds for evaluating the health status.
        /// </summary>
        public int EvaluationTimeInSeconds { get; init; }

        /// <summary>
        /// Gets or sets the maximum number of active requests for the API.
        /// </summary>
        public int ApiMaxActiveRequests { get; init; }

        /// <summary>
        /// Gets or sets the maximum number of history entries per endpoint.
        /// </summary>
        public int MaximumHistoryEntriesPerEndpoint { get; init; }

        /// <summary>
        /// Gets or sets the path to the JSON node where the response status is located.
        /// </summary>
        public string ResponsePath { get; init; }

        /// <summary>
        /// Gets or sets the URL to view the endpoint in a browser.
        /// </summary>
        public string ViewUrl { get; init; }

        /// <summary>
        /// Gets or sets the path to the custom CSS file.
        /// </summary>
        public string CustomCssPath { get; init; }
    }
}
