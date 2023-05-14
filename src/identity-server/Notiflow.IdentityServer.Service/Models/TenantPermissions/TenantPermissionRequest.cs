namespace Notiflow.IdentityServer.Service.Models.TenantPermissions;

public sealed record TenantPermissionRequest
{
    public bool IsSendMessagePermission { get; init; }
    public bool IsSendNotificationPermission { get; init; }
    public bool IsSendEmailPermission { get; init; }
}