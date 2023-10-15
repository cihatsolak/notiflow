namespace Notiflow.Gateway.Infrastructure.Settings;

public sealed record HealthCheckEndpointSetting
{
    public required string Name { get; set; }
    public required string Uri { get; set; }
}
