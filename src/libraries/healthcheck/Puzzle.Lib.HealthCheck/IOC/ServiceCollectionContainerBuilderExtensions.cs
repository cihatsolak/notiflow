namespace Puzzle.Lib.HealthCheck.IOC
{
    internal static class ServiceCollectionContainerBuilderExtensions
    {
        internal static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.EnvironmentName.Equals("Localhost"))
                return services;

            IHealthChecksBuilder healthChecksBuilder = services.AddHealthChecks();
            ArgumentNullException.ThrowIfNull(healthChecksBuilder);

            healthChecksBuilder.AddHangfireCheck();
            healthChecksBuilder.AddRedisCheck("redis connection string");
            healthChecksBuilder.AddNpgSqlDatabaseCheck("npg sql database connection string");
            healthChecksBuilder.AddSystemConfigurationCheck();
            healthChecksBuilder.AddServicesCheck(serviceProvider);
            healthChecksBuilder.AddRabbitMqCheck();

            return services;
        }

        internal static IServiceCollection AddHealthChecksUIMemoryStorage(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(HealthSetting));
            HealthSetting healthSetting = configurationSection.Get<HealthSetting>();

            services.AddHealthChecksUI(setupSettings =>
            {
                setupSettings.SetEvaluationTimeInSeconds(healthSetting.EvaluationTimeInSeconds);
                setupSettings.SetApiMaxActiveRequests(healthSetting.ApiMaxActiveRequests);
                setupSettings.MaximumHistoryEntriesPerEndpoint(healthSetting.MaximumHistoryEntriesPerEndpoint);
                setupSettings.AddWebhookNotification(
                    name: healthSetting.Name,
                    uri: healthSetting.Uri,
                    payload: healthSetting.Payload,
                    restorePayload: healthSetting.RestorePayload
                    );
            }).AddInMemoryStorage();

            return services;
        }
    }
}
