namespace Notiflow.IdentityServer;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
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
