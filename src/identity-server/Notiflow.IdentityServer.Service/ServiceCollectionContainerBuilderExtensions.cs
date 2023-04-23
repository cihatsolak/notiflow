using Notiflow.IdentityServer.Service.Auth;

namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.TryAddSingleton<ITenantService, TenantManager>();
        services.TryAddSingleton<ITokenService, TokenManager>();
        services.TryAddScoped<IAuthService, AuthManager>();

        return services;
    }
}
