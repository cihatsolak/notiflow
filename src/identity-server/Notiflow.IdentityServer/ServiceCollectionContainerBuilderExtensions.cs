namespace Notiflow.IdentityServer;

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
        });

        services.AddJwtAuthentication(options =>
        {
            options.Audiences = jwtTokenSetting.Audiences;
            options.Issuer = jwtTokenSetting.Issuer;
            options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
            options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
            options.SecurityKey = jwtTokenSetting.SecurityKey;
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
            options.MajorVersion = apiVersionSetting.MajorVersion;
            options.MinorVersion = apiVersionSetting.MinorVersion;
        });

        services
            .AddLowercaseRouting()
            .AddGzipResponseFastestCompress()
            .AddHttpSecurityPrecautions(hostEnvironment);
        
        services.AddConfigureHealthChecks();

        return services;
    }
}
