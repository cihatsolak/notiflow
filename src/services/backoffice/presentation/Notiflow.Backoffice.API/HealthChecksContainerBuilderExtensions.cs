using Puzzle.Lib.HealthCheck.Infrastructure;

namespace Notiflow.Backoffice.API;

internal static class HealthChecksContainerBuilderExtensions
{
    internal static IServiceCollection AddBackofficeHealthChecks(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddHealthChecks()
                .AddNpgSqlDatabaseCheck(configuration[$"{nameof(NotiflowDbContext)}:{nameof(SqlSetting.ConnectionString)}"])
                .AddRedisCheck(configuration[$"{nameof(RedisServerSetting)}:{nameof(RedisServerSetting.ConnectionString)}"])
                .AddSystemCheck()
                .AddRabbitMqCheck("amqp://guest:guest@localhost:5672")
                .AddServicesCheck(new List<HealthChecksUrlGroupSetting>
                {
                    new HealthChecksUrlGroupSetting()
                    {
                        Name = "linkedin",
                        ServiceUri = new Uri("https://www.linkedin.com/"),
                        Tags = new[] { "linkedin" }
                    }
                });

        return services;
    }
}
