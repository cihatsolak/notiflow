namespace Notiflow.IdentityServer.Service.Lifetimes;

public static class TenantsInformationCacheApplicationLifetime
{
    private static IServiceProvider ServiceProvider { get; set; }
    private static ILogger Logger => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(TenantsInformationCacheApplicationLifetime));
    private static IRedisService RedisService => ServiceProvider.GetRequiredService<IRedisService>();
    private static IHostApplicationLifetime HostApplicationLifetime => ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
    private static ITenantCacheKeyGenerator TenantCacheKeyGenerator => ServiceProvider.GetRequiredService<ITenantCacheKeyGenerator>();

    public static IApplicationBuilder CacheTenantsInformation(this IApplicationBuilder applicationBuilder)
    {
        ServiceProvider = applicationBuilder.ApplicationServices;
       
        HostApplicationLifetime.ApplicationStarted.Register(OnStarted);

        return applicationBuilder;
    }

    private static void OnStarted()
    {
        Task.Run(TenantsInformationCache);
    }

    private static async Task TenantsInformationCache()
    {
        await using AsyncServiceScope asyncServiceScope = ServiceProvider.CreateAsyncScope();
        ApplicationDbContext context = asyncServiceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            var tenants = await context.Tenants
                            .IgnoreQueryFilters()
                            .AsNoTracking()
                            .Include(p => p.TenantApplication)
                            .Include(p => p.TenantPermission)
                            .ToListAsync(CancellationToken.None);

            if (tenants.IsNullOrNotAny())
            {
                Logger.LogError("The tenants information could not be found in the database.");
                return;
            }

            List<Task<bool>> tenantCachingTasks = new();

            foreach (var tenant in tenants.OrEmptyIfNull())
            {
                string cacheKey = TenantCacheKeyGenerator.GenerateCacheKey(RedisCacheKeys.TENANT_PERMISSION, tenant.Token);

                tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.MESSAGE_PERMISSION, tenant.TenantPermission.IsSendMessagePermission));
                tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.EMAIL_PERMISSION, tenant.TenantPermission.IsSendEmailPermission));
                tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, RedisCacheKeys.NOTIFICATION_PERMISSION, tenant.TenantPermission.IsSendNotificationPermission));
            }

            foreach (var tenant in tenants.OrEmptyIfNull())
            {
                string cacheKey = TenantCacheKeyGenerator.GenerateCacheKey(RedisCacheKeys.TENANT_APPS_INFORMATION, tenant.Token);

                tenantCachingTasks.Add(RedisService.SetAsync(cacheKey, tenant.Adapt<TenantApplicationCacheModel>()));
            }

            await Task.WhenAll(tenantCachingTasks);

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
