namespace Notiflow.Backoffice.API;

internal static class ServiceCollectionContainerBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        IHostEnvironment hostEnvironment = services.BuildServiceProvider().GetRequiredService<IHostEnvironment>();

        JwtTokenSetting jwtTokenSetting = configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();
        SwaggerSetting swaggerSetting = configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
        ApiVersionSetting apiVersionSetting = configuration.GetRequiredSection(nameof(ApiVersionSetting)).Get<ApiVersionSetting>();

        var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
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
            options.AddPolicy(PolicyName.TEXT_MESSAGE_PERMISSON_RESTRICTION, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new MessagePermissionRequirement());
            });

            options.AddPolicy(PolicyName.NOTIFICATION_PERMISSION_RESTRICTION, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new NotificationPermissionRequirement());
            });

            options.AddPolicy(PolicyName.EMAIL_PERMISSION_RESTRICTION, policy =>
            {
                policy.RequireAuthenticatedUser();
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

        services
            .AddLowercaseRouting()
            .AddGzipResponseFastestCompress()
            .AddHttpSecurityPrecautions(hostEnvironment);

        services.AddBackofficeHealthChecks();

        return services;
    }
}
