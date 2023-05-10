namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        AddLibraries(services);
        AddSingletionServices(services);
        AddScopedServices(services);
        
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
    }
}
