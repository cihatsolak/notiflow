using Notiflow.Common.Services;

namespace Notiflow.Common;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddTenantCacheKeyGenerator(this IServiceCollection services)
    {
        services.TryAddSingleton<ITenantCacheKeyGenerator, TenantCacheKeyGenerator>();

        return services;
    }
}
