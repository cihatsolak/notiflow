namespace Notiflow.IdentityServer.Service.Models;

public sealed record TenantPermissionRequest
{
    public bool IsSendMessagePermission { get; init; }
    public bool IsSendNotificationPermission { get; init; }
    public bool IsSendEmailPermission { get; init; }
}