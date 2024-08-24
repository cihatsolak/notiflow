namespace Notiflow.IdentityServer.Service.Models;

public sealed record TenantPermissionRequest(bool IsSendMessagePermission, bool IsSendNotificationPermission, bool IsSendEmailPermission);

public sealed record TenantPermissionRequestExample : IExamplesProvider<TenantPermissionRequest>
{
    public TenantPermissionRequest GetExamples() => new(true, false, true);
}