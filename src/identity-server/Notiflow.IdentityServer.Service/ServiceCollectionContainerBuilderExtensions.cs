namespace Notiflow.IdentityServer.Service;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.TryAddSingleton<ITenantService, TenantManager>();

        return services;
    }
}
