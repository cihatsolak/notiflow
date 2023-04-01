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
        public required string Name { get; init; }

        /// <summary>
        /// Gets or sets the URI of the endpoint.
        /// </summary>
        public required string Uri { get; init; }

        /// <summary>
        /// Gets or sets the payload to send in the request.
        /// </summary>
        public required string Payload { get; init; }

        /// <summary>
        /// Gets or sets the restore payload to send in the request to restore the service.
        /// </summary>
        public required string RestorePayload { get; init; }

        /// <summary>
        /// Gets or sets the time interval in seconds for evaluating the health status.
        /// </summary>
        public required int EvaluationTimeInSeconds { get; init; }

        /// <summary>
        /// Gets or sets the maximum number of active requests for the API.
        /// </summary>
        public required int ApiMaxActiveRequests { get; init; }

        /// <summary>
        /// Gets or sets the maximum number of history entries per endpoint.
        /// </summary>
        public required int MaximumHistoryEntriesPerEndpoint { get; init; }

        /// <summary>
        /// Gets or sets the path to the JSON node where the response status is located.
        /// </summary>
        public required string ResponsePath { get; init; }
    }
}
