namespace Notiflow.IdentityServer.Service.TenantPermissions;

public interface ITenantPermissionService
{
    Task<ApiResponse<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<ApiResponse<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
}
