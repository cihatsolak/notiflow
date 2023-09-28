namespace Notiflow.IdentityServer.Data;

public static class ServiceCollectionContainerBuilderExtensions
{
    public static IServiceCollection AddDataDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        SqlSetting sqlSetting = configuration.GetRequiredSection(nameof(ApplicationDbContext)).Get<SqlSetting>();

        services.AddMicrosoftSql<ApplicationDbContext>(options =>
        {
            options.IsProduction = sqlSetting.IsProduction;
            options.IsSplitQuery = sqlSetting.IsSplitQuery;
            options.ConnectionString = sqlSetting.ConnectionString;
            options.CommandTimeoutSecond = sqlSetting.CommandTimeoutSecond;
        });

        services.SeedAsync().Wait();
       
        return services;
    }
}
