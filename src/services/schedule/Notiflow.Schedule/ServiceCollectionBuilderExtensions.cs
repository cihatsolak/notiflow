namespace Notiflow.Schedule;

internal static class ServiceCollectionBuilderExtensions
{
    internal static WebApplicationBuilder AddWebDependencies(this WebApplicationBuilder builder)
    {
        SqlSetting sqlSetting = builder.Configuration.GetRequiredSection(nameof(ScheduledDbContext)).Get<SqlSetting>();
        HangfireSetting hangfireSetting = builder.Configuration.GetRequiredSection(nameof(HangfireSetting)).Get<HangfireSetting>();
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

        builder.Services.AddMicrosoftSql<ScheduledDbContext>(options =>
        {
            options.ConnectionString = sqlSetting.ConnectionString;
            options.CommandTimeoutSecond = sqlSetting.CommandTimeoutSecond;
            options.IsSplitQuery = sqlSetting.IsSplitQuery;
        });

        builder.Services.AddHangfireMsSql(options =>
        {
            options.ConnectionString = hangfireSetting.ConnectionString;
            options.GlobalAutomaticRetryAttempts = hangfireSetting.GlobalAutomaticRetryAttempts;
            options.Username = hangfireSetting.Username;
            options.Password = hangfireSetting.Password;
        });

        builder.Services.AddApiVersion(options =>
        {
            options.MajorVersion = apiVersionSetting.MajorVersion;
            options.MinorVersion = apiVersionSetting.MinorVersion;
        });

        if (!builder.Environment.IsProduction())
        {
            builder.Services.AddSwagger(options =>
            {
                options.Title = swaggerSetting.Title;
                options.Description = swaggerSetting.Description;
                options.Version = swaggerSetting.Version;
                options.ContactName = swaggerSetting.ContactName;
                options.ContactEmail = swaggerSetting.ContactEmail;
            });
        }

        return builder;
    }

    internal static WebApplicationBuilder AddConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
                        .AddMsSqlDatabaseCheck(builder.Configuration[$"{nameof(ScheduledDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
                        .AddRedisCheck(builder.Configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
                        .AddSystemCheck()
                        .AddHangfireCheck();

        return builder;
    }
}
