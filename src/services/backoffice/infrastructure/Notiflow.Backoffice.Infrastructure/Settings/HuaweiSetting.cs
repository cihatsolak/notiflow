namespace Notiflow.Backoffice.Infrastructure.Settings;

internal sealed record HuaweiSetting
{
    [JsonRequired]
    public required string SendServiceUrl { get; init; }

    [JsonRequired]
    public required string AuthTokenServiceUrl { get; init; }
}
