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
            cfg.DefaultRequestHeaders.Add("x-tenant-token", "F50C77DB-6C3B-43E6-BEE2-2142A71E04E4");
        }).AddHttpMessageHandler<TenantHandler>();

        services.AddHttpClient(nameof(AuthManager), cfg =>
        {
            cfg.BaseAddress = new Uri("https://localhost:7282");
            cfg.DefaultRequestHeaders.Add("x-tenant-token", "F50C77DB-6C3B-43E6-BEE2-2142A71E04E4");
        });

        return services;
    }
}
