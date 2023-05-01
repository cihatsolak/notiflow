﻿namespace Notiflow.IdentityServer.Service.Tenants;

public interface ITenantPermissionService
{
    Task<ResponseData<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<Response> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken);
}