namespace Notiflow.Backoffice.API;

internal static class ServiceCollectionBuilderExtensions
{
    internal static IServiceCollection AddWebDependencies(this WebApplicationBuilder builder)
    {
        JwtTokenSetting jwtTokenSetting = builder.Configuration.GetRequiredSection(nameof(JwtTokenSetting)).Get<JwtTokenSetting>();
        SwaggerSetting swaggerSetting = builder.Configuration.GetRequiredSection(nameof(SwaggerSetting)).Get<SwaggerSetting>();
        ApiVersionSetting apiVersionSetting = builder.Configuration.GetRequiredSection(nameof(ApiVersionSetting)).Get<ApiVersionSetting>();

        var authorizationPolicy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .RequireClaim(ClaimTypes.NameIdentifier)
                                    .Build();

        builder.Services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
            options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
            options.Filters.Add<TenantTokenAuthenticationFilter>();
        });

        builder.Services.AddJwtAuthentication(options =>
        {
            options.Audiences = jwtTokenSetting.Audiences;
            options.Issuer = jwtTokenSetting.Issuer;
            options.AccessTokenExpirationMinute = jwtTokenSetting.AccessTokenExpirationMinute;
            options.RefreshTokenExpirationMinute = jwtTokenSetting.RefreshTokenExpirationMinute;
            options.SecurityKey = jwtTokenSetting.SecurityKey;
        });

        builder.Services.AddAuthorization(options =>
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

        builder.Services.AddSignalConfiguration(builder.Configuration);
       
        if (!builder.Environment.IsProduction())
        {
            builder.Services.AddHttpSecurityPrecautions();
        }

        return builder.Services;
    }

    internal static IServiceCollection AddBackofficeHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
                .AddNpgSqlDatabaseCheck(builder.Configuration[$"{nameof(NotiflowDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
                .AddRedisCheck(builder.Configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
                .AddSystemCheck()
                .AddRabbitMqCheck("amqp://guest:guest@localhost:5672")
                .AddServicesCheck(new List<HealthChecksUrlGroupSetting>
                {
                    new HealthChecksUrlGroupSetting()
                    {
                        Name = "linkedin",
                        ServiceUri = new Uri("https://www.linkedin.com/"),
                        Tags = ["linkedin"]
                    }
                });

        return builder.Services;
    }
}
