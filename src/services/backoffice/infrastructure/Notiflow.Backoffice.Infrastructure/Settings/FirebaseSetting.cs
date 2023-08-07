namespace Notiflow.Backoffice.Infrastructure.Settings;

internal sealed record FirebaseSetting
{
    public required Uri BaseAddress { get; init; }
}
