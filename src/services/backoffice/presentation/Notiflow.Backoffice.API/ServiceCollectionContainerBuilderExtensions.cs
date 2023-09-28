namespace Notiflow.Backoffice.API;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        JwtTokenSetting jwtTokenSetting = configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();

        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add<TenantTokenAuthenticationFilter>();
        });

        services.AddJwtAuthentication(options =>
        {
            options.Audiences = jwtTokenSetting.Audiences;
            options.Issuer = jwtTokenSetting.Issuer;
            options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
            options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
            options.SecurityKey = jwtTokenSetting.SecurityKey;
        });

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
