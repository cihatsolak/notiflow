namespace Notiflow.Panel;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddHttpServices(this IServiceCollection services)
    {
        services.TryAddScoped<TenantHandler>();

        services.AddRestApiService();
        services.AddHttpClient("notiflow.api", cfg =>
        {
            cfg.BaseAddress = new Uri("https://localhost:7282");
        }).AddHttpMessageHandler<TenantHandler>();

        return services;
    }
}
