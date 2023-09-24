namespace Notiflow.IdentityServer;

internal static class HealthChecksContainerBuilderExtensions
{
    internal static IServiceCollection AddConfigureHealthChecks(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddHealthChecks()
                .AddMsSqlDatabaseCheck(configuration["ApplicationDbContext:ConnectionString"])
                .AddRedisCheck(configuration["RedisServerSetting:ConnectionString"])
                .AddSystemCheck();

        return services;
    }
}
