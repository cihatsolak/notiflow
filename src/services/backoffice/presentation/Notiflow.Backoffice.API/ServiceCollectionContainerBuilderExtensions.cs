using Puzzle.Lib.Auth.Infrastructure;

namespace Notiflow.Backoffice.API;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        JwtTokenSetting jwtTokenSetting = configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();
        SwaggerSetting swaggerSetting = configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
        ApiVersionSetting apiVersionSetting = configuration.GetRequiredSection(nameof(ApiVersionSetting)).Get<ApiVersionSetting>();

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

       
        services.AddSwagger(options =>
        {
            options.Title = swaggerSetting.Title;
            options.Description = swaggerSetting.Description;
            options.Version = swaggerSetting.Version;
            options.ContactName = swaggerSetting.ContactName;
            options.ContactEmail = swaggerSetting.ContactEmail;
        });

        services.AddApiVersion(options =>
        {
            options.HeaderName = apiVersionSetting.HeaderName;
            options.MajorVersion = apiVersionSetting.MajorVersion;
            options.MinorVersion = apiVersionSetting.MinorVersion;
        });

        services.AddRouteSettings();
        services.AddGzipResponseFastestCompress();
        services.AddHttpSecurityPrecautions(services.BuildServiceProvider().GetRequiredService<IWebHostEnvironment>());

        services.AddConfigureHealthChecks();

        return services;
    }
}
