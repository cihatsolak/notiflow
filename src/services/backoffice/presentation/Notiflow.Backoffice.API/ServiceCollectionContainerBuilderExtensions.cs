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

        services.AddAuthorization(options =>
        {
            options.AddPolicy("TextMessagePermissionRestriction", policy =>
            {
                policy.AddRequirements(new MessagePermissionRequirement());
            });

            options.AddPolicy("NotificationPermissionRestriction", policy =>
            {
                policy.AddRequirements(new NotificationPermissionRequirement());
            });

            options.AddPolicy("EmailPermissionRestriction", policy =>
            {
                policy.AddRequirements(new EmailPermissionRequirement());
            });
        });

        services.AddRouteSettings();
        services.AddSwagger();
        services.AddGzipResponseFastestCompress();
        services.AddHttpSecurityPrecautions();
        services.AddApiVersion();
        services.AddConfigureHealthChecks();

        return services;
    }
}
