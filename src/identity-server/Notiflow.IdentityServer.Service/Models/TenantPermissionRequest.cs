namespace Notiflow.IdentityServer.Service.Models;

public sealed record TenantPermissionRequest(bool IsSendMessagePermission, bool IsSendNotificationPermission, bool IsSendEmailPermission);