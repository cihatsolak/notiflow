namespace Notiflow.Backoffice.API;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add<TenantTokenAuthenticationFilter>();
        });

        services.AddJwtAuthentication();
        services.AddRouteSettings();
        services.AddSwagger();
        services.AddGzipResponseFastestCompress();
        services.AddHttpSecurityPrecautions();
        services.AddApiVersion();

        return services;
    }
}
