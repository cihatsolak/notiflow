using Notiflow.IdentityServer.Service.Models.TenantPermissions;

namespace Notiflow.IdentityServer.Service.TenantPermissions;

public interface ITenantPermissionService
{
    Task<ResponseData<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<Response> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
}
