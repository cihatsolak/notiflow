namespace Notiflow.IdentityServer.Service.TenantPermissions;

internal sealed class TenantPermissionManager : ITenantPermissionService
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisService _redisService;
    private readonly ILocalizerService<TenantPermission> _localizer;
    private readonly ILogger<TenantPermissionManager> _logger;

    public TenantPermissionManager(
        ApplicationDbContext context,
        IRedisService redisService,
        ILocalizerService<TenantPermission> localizer,
        ILogger<TenantPermissionManager> logger)
    {
        _context = context;
        _redisService = redisService;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.AsNoTracking()
                                                               .ProjectToType<TenantPermissionResponse>()
                                                               .SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            return Result<TenantPermissionResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.TENANT_PERMISSION_NOT_FOUND]);
        }

        return Result<TenantPermissionResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], tenantPermission);
    }

    public async Task<Result<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.TENANT_PERMISSION_NOT_FOUND]);
        }

        List<Task<bool>> tenantPermissionCachingTasks = new();
        string cacheKey = TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO);

        if (tenantPermission.IsSendMessagePermission != request.IsSendMessagePermission)
        {
            tenantPermissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_MESSAGE_PERMISSION, request.IsSendMessagePermission));
        }

        if (tenantPermission.IsSendNotificationPermission != request.IsSendNotificationPermission)
        {
            tenantPermissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_EMAIL_PERMISSION, request.IsSendEmailPermission));
        }

        if (tenantPermission.IsSendEmailPermission != request.IsSendEmailPermission)
        {
            tenantPermissionCachingTasks.Add(_redisService.HashSetAsync(cacheKey, CacheKeys.TENANT_NOTIFICATION_PERMISSION, request.IsSendNotificationPermission));
        }

        tenantPermission.IsSendMessagePermission = request.IsSendMessagePermission;
        tenantPermission.IsSendNotificationPermission = request.IsSendNotificationPermission;
        tenantPermission.IsSendEmailPermission = request.IsSendEmailPermission;

        await _context.SaveChangesAsync(cancellationToken);

        await Task.WhenAll(tenantPermissionCachingTasks);

        return Result<EmptyResponse>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.TENANT_PERMISSION_NOT_FOUND]);
    }
}
