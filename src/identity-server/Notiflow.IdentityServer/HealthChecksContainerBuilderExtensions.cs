namespace Notiflow.IdentityServer;

internal static class HealthChecksContainerBuilderExtensions
{
    internal static void AddConfigureHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services
               .AddHealthChecks()
               .AddMsSqlDatabaseCheck(builder.Configuration[$"{nameof(ApplicationDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
               .AddRedisCheck(builder.Configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
               .AddSystemCheck();
    }
}
