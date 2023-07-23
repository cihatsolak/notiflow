namespace Notiflow.IdentityServer.Service.Lifetimes;

public static class TenantInformationCAcheApplicationLifetime
{
    private static ILogger Logger { get; set; }
    private static IRedisService RedisService { get; set; }
    private static IHostEnvironment HostEnvironment { get; set; }
    private static IHostApplicationLifetime HostApplicationLifetime { get; set; }
    private static IServiceProvider ServiceProvider { get; set; }

    public static IApplicationBuilder CacheTenantInformation(this IApplicationBuilder applicationBuilder)
    {
        SetUpServices(applicationBuilder);
        HostApplicationLifetime.ApplicationStarted.Register(OnStarted);
        return applicationBuilder;
    }

    private static void OnStarted()
    {
        Task.Run(CacheTenantInformation);
    }

    private static void SetUpServices(IApplicationBuilder applicationBuilder)
    {
        ServiceProvider = applicationBuilder.ApplicationServices;
        Logger = ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(nameof(TenantInformationCAcheApplicationLifetime));
        RedisService = ServiceProvider.GetRequiredService<IRedisService>();
        HostEnvironment = ServiceProvider.GetRequiredService<IHostEnvironment>();
        HostApplicationLifetime = ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
    }

    private static async Task CacheTenantInformation()
    {
        string applicationName = HostEnvironment.ApplicationName;
        string environmentName = HostEnvironment.EnvironmentName;

        await using AsyncServiceScope asyncServiceScope = ServiceProvider.CreateAsyncScope();
        ApplicationDbContext context = asyncServiceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        try
        {
            var asd = await RedisService.HashGetAsync<bool>("0A3D33A2-7F29-43AB-A33E-5DED8E759BD8", "tenant.message.permission");

            var tenantCacheModels = await context.Tenants
                            .IgnoreQueryFilters()
                            .AsNoTracking()
                            .Include(p => p.TenantApplication)
                            .Include(p => p.TenantPermission)
                            .ProjectToType<TenantCacheModel>()
                            .ToListAsync(CancellationToken.None);

            if (tenantCacheModels.IsNullOrEmpty())
            {
                Logger.LogError("No tenant information found in the database. Application Name: {@applicationName} - Environment Name: {@environmentName}", applicationName, environmentName);
                return;
            }

            List<Task<bool>> tenantCachingTasks = new();

            foreach (var tenant in tenantCacheModels.OrEmptyIfNull())
            {
                tenantCachingTasks.Add(RedisService.HashSetAsync(tenant.Token, RedisCacheKeys.TENANT_MESSAGE_PERMISSION, $"{tenant.IsSendMessagePermission}"));
                tenantCachingTasks.Add(RedisService.HashSetAsync(tenant.Token, RedisCacheKeys.TENANT_EMAIL_PERMISSION, $"{tenant.IsSendEmailPermission}"));
                tenantCachingTasks.Add(RedisService.HashSetAsync(tenant.Token, RedisCacheKeys.TENANT_NOTIFICATION_PERMISSION, $"{tenant.IsSendNotificationPermission}"));
            }

            await Task.WhenAll(tenantCachingTasks);

            Logger.LogInformation("Tenant information has been added to the cache. Application Name: {@applicationName} - Environment Name: {@environmentName}", applicationName, environmentName);

        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "An error occurred while adding tenant information to the cache. Application Name: {@applicationName} - Environment Name: {@environmentName}", applicationName, environmentName);
        }
        finally
        {
            await asyncServiceScope.DisposeAsync();
            await context.DisposeAsync();
        }
    }
}
