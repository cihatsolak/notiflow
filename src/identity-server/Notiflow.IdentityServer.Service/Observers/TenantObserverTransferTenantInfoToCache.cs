namespace Notiflow.IdentityServer.Service.Observers;

internal class TenantObserverTransferTenantInfoToCache : ITenantObserver
{
    private readonly ApplicationDbContext _context;
    private readonly IRedisService _redisService;
    private readonly ILogger<TenantObserverTransferTenantInfoToCache> _logger;

    public TenantObserverTransferTenantInfoToCache(IServiceProvider serviceProvider)
    {
        _context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        _redisService = serviceProvider.GetRequiredService<IRedisService>();
        _logger = serviceProvider.GetRequiredService<ILogger<TenantObserverTransferTenantInfoToCache>>();
    }

    public async Task ExecuteAsync(Guid tenantToken)
    {
        var tenant = await _context.Tenants
                           .IgnoreQueryFilters()
                           .AsNoTracking()
                           .Include(p => p.TenantApplication)
                           .Include(p => p.TenantPermission)
                           .SingleOrDefaultAsync(tenant => tenant.Token == tenantToken, CancellationToken.None);

        if (tenant is null)
        {
            _logger.LogError("No tenant information found in the database.");
            return;
        }

        string tenantTokenCacheKey = tenantToken.ToString();

        List<Task<bool>> tenantCachingTasks = new()
        {
            //_redisService.HashSetAsync(tenantTokenCacheKey, RedisCacheKeys.TENANT_MESSAGE_PERMISSION, tenant.TenantPermission.IsSendMessagePermission),
            //_redisService.HashSetAsync(tenantTokenCacheKey, RedisCacheKeys.TENANT_EMAIL_PERMISSION, tenant.TenantPermission.IsSendEmailPermission),
            //_redisService.HashSetAsync(tenantTokenCacheKey, RedisCacheKeys.TENANT_NOTIFICATION_PERMISSION, tenant.TenantPermission.IsSendNotificationPermission)
        };

        await Task.WhenAll(tenantCachingTasks);

        _logger.LogInformation("The information of the {@tenantName} tenant with ID {@tenantId} was transferred to the redis.", tenant.Name, tenant.Id);
    }
}
