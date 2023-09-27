namespace Notiflow.Backoffice.API;

internal static class HealthChecksContainerBuilderExtensions
{
    internal static IServiceCollection AddConfigureHealthChecks(this IServiceCollection services)
    {
        IConfiguration configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

        services.AddHealthChecks()
                .AddNpgSqlDatabaseCheck(configuration["NotiflowDbContext:ConnectionString"])
                .AddRedisCheck(configuration["RedisServerSetting:ConnectionString"])
                .AddSystemCheck()
                .AddRabbitMqCheck("amqp://guest:guest@localhost:5672")
                .AddServicesCheck(new List<Puzzle.Lib.HealthCheck.Settings.HealthChecksUrlGroupSetting>
                {
                    new Puzzle.Lib.HealthCheck.Settings.HealthChecksUrlGroupSetting()
                    {
                        Name = "linkedin",
                        ServiceUri = new Uri("https://www.linkedin.com/"),
                        Tags = new[] { "linkedin" }
                    }
                });

        return services;
    }
}
