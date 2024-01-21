﻿namespace Notiflow.IdentityServer.Data;

public static class ServiceCollectionBuilderExtensions
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        SqlSetting sqlSetting = configuration.GetRequiredSection(nameof(ApplicationDbContext)).Get<SqlSetting>();

        services.AddMicrosoftSql<ApplicationDbContext>(options =>
        {
            options.IsSplitQuery = sqlSetting.IsSplitQuery;
            options.ConnectionString = sqlSetting.ConnectionString;
            options.CommandTimeoutSecond = sqlSetting.CommandTimeoutSecond;
        });

        services.SeedAsync(CancellationToken.None).Wait();

        return services;
    }
}
