namespace Notiflow.IdentityServer.Service.TenantPermissions;

public interface ITenantPermissionService
{
    Task<Result<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<Result> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
}
