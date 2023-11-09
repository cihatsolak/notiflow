namespace Notiflow.Gateway.Infrastructure.Settings;

public sealed record HealthCheckSetting
{
    public string StorageConnectionString { get; set; }
    public List<HealthCheckEndpointSetting> EndpointControlSetting { get; set; }
    public HealthCheckSlackWebHookSetting SlackWebHookSetting { get; set; }

}

public sealed record HealthCheckEndpointSetting
{
    public required string Name { get; set; }
    public required string Uri { get; set; }
}


public sealed record HealthCheckSlackWebHookSetting
{
    public string Uri { get; init; }
    public TimeSpan NotificationSendingStartTime { get; init; }
    public TimeSpan NotificationSendingEndTime { get; init; }
}