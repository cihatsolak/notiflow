namespace Notiflow.IdentityServer.Service.TenantPermissions;

internal sealed class TenantPermissionManager : ITenantPermissionService
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisService _redisService;
    private readonly ILogger<TenantPermissionManager> _logger;

    public TenantPermissionManager(
        ApplicationDbContext context,
        IRedisService redisService,
        ILogger<TenantPermissionManager> logger)
    {
        _context = context;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<Result<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions
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

    public async Task<Result<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions
            .TagWith("Get tenant's permission.")
            .SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            return Result<EmptyResponse>.Status404NotFound(ResultCodes.TENANT_PERMISSION_NOT_FOUND);
        }

        List<Task<bool>> permissionCachingTasks = [];
        string cacheKey = TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO);

        if (tenantPermission.IsSendMessagePermission != request.IsSendMessagePermission)
        {
            tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
            permissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_MESSAGE_PERMISSION, request.IsSendMessagePermission));
        }

        if (tenantPermission.IsSendNotificationPermission != request.IsSendNotificationPermission)
        {
            tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
            permissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_EMAIL_PERMISSION, request.IsSendEmailPermission));
        }

        if (tenantPermission.IsSendEmailPermission != request.IsSendEmailPermission)
        {
            tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;
            permissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_NOTIFICATION_PERMISSION, request.IsSendNotificationPermission));
        }

        await _context.SaveChangesAsync(cancellationToken);
        await Task.WhenAll(permissionCachingTasks);

        _logger.LogInformation("Permission information for {tenantId} tenant with ID has been updated.", tenantPermission.TenantId);

        return Result<EmptyResponse>.Status204NoContent(ResultCodes.TENANT_PERMISSION_NOT_FOUND);
    }
}
