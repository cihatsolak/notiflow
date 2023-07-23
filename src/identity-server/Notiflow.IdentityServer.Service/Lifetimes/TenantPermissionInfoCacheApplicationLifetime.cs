namespace Notiflow.IdentityServer.Service.Lifetimes;

public static class TenantPermissionInfoCacheApplicationLifetime
{
    private static IServiceProvider ServiceProvider { get; set; }
    private static ILogger Logger => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(TenantPermissionInfoCacheApplicationLifetime));
    private static IRedisService RedisService => ServiceProvider.GetRequiredService<IRedisService>();
    private static IHostApplicationLifetime HostApplicationLifetime => ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
    private static ITenantCacheKeyGenerator TenantCacheKeyGenerator => ServiceProvider.GetRequiredService<ITenantCacheKeyGenerator>();

    public static IApplicationBuilder CacheTenatPermissionInformation(this IApplicationBuilder applicationBuilder)
    {
        ServiceProvider = applicationBuilder.ApplicationServices;
       
        HostApplicationLifetime.ApplicationStarted.Register(OnStarted);

        return applicationBuilder;
    }

    private static void OnStarted()
    {
        Task.Run(TenantPermissionInfoCache);
    }

    private static async Task TenantPermissionInfoCache()
    {
        await using AsyncServiceScope asyncServiceScope = ServiceProvider.CreateAsyncScope();
        ApplicationDbContext context = asyncServiceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            var tenants = await context.Tenants
                            .IgnoreQueryFilters()
                            .AsNoTracking()
                            .Include(p => p.TenantPermission)
                            .ToListAsync(CancellationToken.None);

            if (tenants.IsNullOrNotAny())
            {
                Logger.LogError("No tenant information found in the database.");
                return;
            }

            List<Task<bool>> tenantPermissionCachingTasks = new();

            foreach (var tenant in tenants.OrEmptyIfNull())
            {
                string cacheKey = TenantCacheKeyGenerator.GenerateCacheKey(RedisCacheKeys.TENANT_PERMISSION, tenant.Token);

                tenantPermissionCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.TENANT_MESSAGE, tenant.TenantPermission.IsSendMessagePermission));
                tenantPermissionCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.TENANT_EMAIL, tenant.TenantPermission.IsSendEmailPermission));
                tenantPermissionCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.TENANT_NOTIFICATION, tenant.TenantPermission.IsSendNotificationPermission));
            }

            await Task.WhenAll(tenantPermissionCachingTasks);

            Logger.LogInformation("Tenant information has been added to the cache.");

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An error occurred while adding tenant information to the cache.");
        }
        finally
        {
            await asyncServiceScope.DisposeAsync();
            await context.DisposeAsync();
        }
    }
}
