using Notiflow.IdentityServer.Service.Models.TenantPermissions;

namespace Notiflow.IdentityServer.Service.TenantPermissions;

public interface ITenantPermissionService
{
    Task<Response<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<Response<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
}
