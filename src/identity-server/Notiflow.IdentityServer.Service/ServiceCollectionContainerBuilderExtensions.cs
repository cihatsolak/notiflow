namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        AddLibraries(services);

        services.TryAddScoped<IAuthService, AuthManager>();

        services.TryAddSingleton<ITenantService, TenantManager>();
        services.TryAddScoped<ITenantPermissionService, TenantPermissionManager>();
        
        services.TryAddSingleton<ITokenService, TokenManager>();

        services.TryAddScoped<IUserService, UserManager>();

        return services;
    }

    private static void AddLibraries(IServiceCollection services)
    {
        services.AddClaimService();
    }
}
