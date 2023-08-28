namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services
            .AddClaimService()
            .AddRedisService()
            .AddFtpService();

        services
            .AddFluentDesignValidation()
            .AddApiBehaviorOptions();

        services.AddHttpContextAccessor();
        services.TryAddSingleton<ITokenService, TokenManager>();

        services.TryAddScoped<IAuthService, AuthManager>();
        services.TryAddScoped<ITenantService, TenantManager>();
        services.TryAddScoped<ITenantPermissionService, TenantPermissionManager>();
        services.TryAddScoped<IUserService, UserManager>();

        AddObservers(services);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }

    private static void AddObservers(IServiceCollection services)
    {
        services.TryAddScoped<ITenantObserverSubject>(provider => //maybe singleton
        {
            TenantObserverSubject tenantObserverSubject = new();
            tenantObserverSubject.RegisterObserver(new TenantObserverTransferTenantInfoToCache(provider));

            return tenantObserverSubject;
        });
    }
}
