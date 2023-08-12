using Notiflow.Common.Extensions;

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

    public async Task<Response<TenantPermissionResponse>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.AsNoTracking()
                                                               .ProjectToType<TenantPermissionResponse>()
                                                               .SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return Response<TenantPermissionResponse>.Fail(-1);
        }

        return Response<TenantPermissionResponse>.Success(tenantPermission);
    }

    public async Task<Response<EmptyResponse>> UpdateAsync(TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var tenantPermission = await _context.TenantPermissions.SingleAsync(cancellationToken);
        if (tenantPermission is null)
        {
            _logger.LogInformation("Tenant permissions not found.");
            return Response<EmptyResponse>.Fail(-1);
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

        return Response<EmptyResponse>.Success(-1);
    }
}
