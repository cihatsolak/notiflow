namespace Notiflow.IdentityServer.Service.Lifetimes;

public static class TenantsInformationCacheApplicationLifetime
{
    private static IServiceProvider ServiceProvider { get; set; }
    private static ILogger Logger => ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(TenantsInformationCacheApplicationLifetime));
    private static IRedisService RedisService => ServiceProvider.GetRequiredService<IRedisService>();
    private static IHostApplicationLifetime HostApplicationLifetime => ServiceProvider.GetRequiredService<IHostApplicationLifetime>();

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
        ITenantService tenantService = asyncServiceScope.ServiceProvider.GetRequiredService<ITenantService>();

        try
        {
            var result = await tenantService.GetTenantsAsync(CancellationToken.None);
            if (result.IsFailed)
                return;

            var transactionResults = await AddCacheAsync(result.Data);
            if (Array.TrueForAll(transactionResults, transaction => transaction))
            {
                Logger.LogInformation("Tenant information has been added to the cache.");
                return;
            }

            await RedisService.RemoveKeysBySearchKeyAsync(CacheKeys.TENANT_INFO, SearchKeyType.StartsWith);
        }
        catch (Exception exception)
        {
            Logger.LogError(exception, "An error occurred while adding tenant information to the cache.");
        }
    }


    private static async Task<bool[]> AddCacheAsync(List<Tenant> tenants)
    {
        List<Task<bool>> tenantCachingTasks = [];

        foreach (var tenant in tenants)
        {
            string cacheKey = TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO, tenant.Token);

            bool isExists = await RedisService.ExistsAsync(cacheKey);
            if (isExists)
            {
                continue;
            }

            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_MESSAGE_PERMISSION, tenant.TenantPermission.IsSendMessagePermission));
            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_EMAIL_PERMISSION, tenant.TenantPermission.IsSendEmailPermission));
            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_NOTIFICATION_PERMISSION, tenant.TenantPermission.IsSendNotificationPermission));

            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_APPS_CONFIG, tenant.Adapt<TenantApplicationCacheModel>()));

            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_TOKEN, tenant.Token));
            tenantCachingTasks.Add(RedisService.HashSetAsync(cacheKey, CacheKeys.TENANT_ID, tenant.Id));

            tenantCachingTasks.Add(RedisService.SetAddAsync(CacheKeys.TENANT_TOKENS, tenant.Token));
        }



        return await Task.WhenAll(tenantCachingTasks);
    }
}
