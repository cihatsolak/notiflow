namespace Notiflow.Common.Settings;

/// <summary>
/// Represents the settings for a RabbitMQ server connection.
/// </summary>
public record RabbitMqStandaloneSetting
{
    /// <summary>
    /// Gets or sets the host name of the RabbitMQ server.
    /// </summary>
    public required string HostAddress { get; init; }

    /// <summary>
    /// Gets or sets the username for authenticating with the RabbitMQ server.
    /// </summary>
    public required string Username { get; init; }

    /// <summary>
    /// Gets or sets the password for authenticating with the RabbitMQ server.
    /// </summary>
    public required string Password { get; init; }
}

/// <summary>
/// Represents the settings for a RabbitMQ cluster connection.
/// </summary>
public sealed record RabbitMqClusterSetting : RabbitMqStandaloneSetting
{
    /// <summary>
    /// Gets or sets the list of addresses for the RabbitMQ nodes in the cluster.
    /// </summary>
    public required List<string> NodeAddresses { get; init; }
}