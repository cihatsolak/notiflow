using Notiflow.Common;
using Puzzle.Lib.Cache;

namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        AddLibraries(services);
        AddSingletionServices(services);
        AddScopedServices(services);

        AddObservers(services);

        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        return services;
    }
    
    private static void AddSingletionServices(IServiceCollection services)
    {
        services.TryAddSingleton<ITenantService, TenantManager>();
        services.TryAddSingleton<ITokenService, TokenManager>();
    }

    private static void AddScopedServices(IServiceCollection services) 
    {
        services.TryAddScoped<IAuthService, AuthManager>();
        services.TryAddScoped<ITenantPermissionService, TenantPermissionManager>();
        services.TryAddScoped<IUserService, UserManager>();
    }

    private static void AddLibraries(IServiceCollection services)
    {
        services.AddClaimService();
        services.AddFluentDesignValidation();
        services.AddApiBehaviorOptions();
        services.AddRedisService();
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
