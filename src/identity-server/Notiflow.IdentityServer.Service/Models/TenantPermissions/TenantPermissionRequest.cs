namespace Notiflow.IdentityServer.Service.Models.TenantPermissions;

public sealed record TenantPermissionRequest(bool IsSendMessagePermission, bool IsSendNotificationPermission, bool IsSendEmailPermission);