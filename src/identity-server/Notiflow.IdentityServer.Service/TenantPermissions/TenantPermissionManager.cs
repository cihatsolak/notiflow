namespace Notiflow.IdentityServer.Service.TenantPermissions;

internal sealed class TenantPermissionManager(
    ApplicationDbContext context,
    IRedisService redisService,
    ILogger<TenantPermissionManager> logger) : ITenantPermissionService
{
    public async Task<Result<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await context.TenantPermissions
            .TagWith("Get tenant's permission.")
            .AsNoTracking()
            .ProjectToType<TenantPermissionResponse>()
            .SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            return Result<TenantPermissionResponse>.Status404NotFound(ResultCodes.TENANT_PERMISSION_NOT_FOUND);
        }

        return Result<TenantPermissionResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, tenantPermission);
    }

    public async Task<Result> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await context.TenantPermissions
            .TagWith("Get tenant's permission.")
            .SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            return Result.Status404NotFound(ResultCodes.TENANT_PERMISSION_NOT_FOUND);
        }

        List<Task<bool>> permissionCachingTasks = [];
        string cacheKey = TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO);

        if (tenantPermission.IsSendMessagePermission != request.IsSendMessagePermission)
        {
            tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
            permissionCachingTasks.Add(redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_MESSAGE_PERMISSION, request.IsSendMessagePermission));
        }

        if (tenantPermission.IsSendNotificationPermission != request.IsSendNotificationPermission)
        {
            tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
            permissionCachingTasks.Add(redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_EMAIL_PERMISSION, request.IsSendEmailPermission));
        }

        if (tenantPermission.IsSendEmailPermission != request.IsSendEmailPermission)
        {
            tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;
            permissionCachingTasks.Add(redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_NOTIFICATION_PERMISSION, request.IsSendNotificationPermission));
        }

        await context.SaveChangesAsync(cancellationToken);
        await Task.WhenAll(permissionCachingTasks);

        logger.LogInformation("Permission information for {tenantId} tenant with ID has been updated.", tenantPermission.TenantId);

        return Result.Status204NoContent();
    }
}
