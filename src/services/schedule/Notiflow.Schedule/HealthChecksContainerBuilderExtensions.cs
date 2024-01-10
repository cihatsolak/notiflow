using Puzzle.Lib.HealthCheck.Infrastructure;

namespace Notiflow.Schedule;

internal static class HealthChecksContainerBuilderExtensions
{
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
