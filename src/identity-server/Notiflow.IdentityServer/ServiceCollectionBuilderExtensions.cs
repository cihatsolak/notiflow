namespace Notiflow.IdentityServer;

internal static class ServiceCollectionBuilderExtensions
{
    internal static void AddWebDependencies(this WebApplicationBuilder builder)
    {
        JwtTokenSetting jwtTokenSetting = builder.Configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();
        SwaggerSetting swaggerSetting = builder.Configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
        ApiVersionSetting apiVersionSetting = builder.Configuration.GetRequiredSection(nameof(ApiVersionSetting)).Get<ApiVersionSetting>();

        var authorizationPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
        });

        builder.Services.AddJwtAuthentication(options =>
        {
            options.Audiences = jwtTokenSetting.Audiences;
            options.Issuer = jwtTokenSetting.Issuer;
            options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
            options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
            options.SecurityKey = jwtTokenSetting.SecurityKey;
        });

        builder.Services.AddSwagger(options =>
        {
            options.Title = swaggerSetting.Title;
            options.Description = swaggerSetting.Description;
            options.Version = swaggerSetting.Version;
            options.ContactName = swaggerSetting.ContactName;
            options.ContactEmail = swaggerSetting.ContactEmail;
        });

        builder.Services.AddApiVersion(options =>
        {
            options.MajorVersion = apiVersionSetting.MajorVersion;
            options.MinorVersion = apiVersionSetting.MinorVersion;
        });

        builder.Services
            .AddLowercaseRoute()
            .AddResponseCompress()
            .AddCustomHttpLogging();

        if (!builder.Environment.IsProduction())
        {
            builder.Services.AddHttpSecurityPrecautions();
        }
    }

    internal static void AddConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddHealthChecks()
               .AddMsSqlDatabaseCheck(builder.Configuration[$"{nameof(ApplicationDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
               .AddRedisCheck(builder.Configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
               .AddSystemCheck();
    }
}
