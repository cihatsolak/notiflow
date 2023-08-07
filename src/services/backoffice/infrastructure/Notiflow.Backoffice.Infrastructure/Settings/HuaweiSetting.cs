namespace Notiflow.Backoffice.Infrastructure.Settings;

internal sealed record HuaweiSetting
{
    [JsonRequired]
    public required Uri BaseAddress { get; init; }

    [JsonRequired]
    public required string AuthenticationRoute { get; set; }

    [JsonRequired]
    public required string NotificationRoute { get; set; }
}
