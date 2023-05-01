using Notiflow.IdentityServer.Service.Auth;
using Puzzle.Lib.Auth.IOC;

namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.TryAddSingleton<ITenantService, TenantManager>();
        services.TryAddSingleton<ITokenService, TokenManager>();

        services.TryAddScoped<IAuthService, AuthManager>();

        services.AddClaimService();

        return services;
    }
}
