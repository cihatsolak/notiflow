namespace Notiflow.Backoffice.Infrastructure.Settings;

internal sealed record FirebaseSetting
{
    [JsonRequired]
    public required Uri BaseAddress { get; init; }

    [JsonRequired]
    public required string Route { get; set; }
}
