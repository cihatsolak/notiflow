using Puzzle.Lib.HealthCheck.Infrastructure;

namespace Notiflow.IdentityServer;

internal static class HealthChecksContainerBuilderExtensions
{
    internal static IServiceCollection AddConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHealthChecks()
                .AddMsSqlDatabaseCheck(configuration[$"{nameof(ApplicationDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
                .AddRedisCheck(configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
                .AddSystemCheck();

        return services;
    }
}
