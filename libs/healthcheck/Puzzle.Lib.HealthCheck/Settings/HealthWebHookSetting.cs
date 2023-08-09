namespace Puzzle.Lib.HealthCheck.Settings;

/// <summary>
/// Represents a health check setting for a specific endpoint.
/// </summary>
internal sealed record HealthWebHookSetting
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
}
