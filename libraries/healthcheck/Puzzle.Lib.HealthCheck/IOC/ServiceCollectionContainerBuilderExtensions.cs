namespace Puzzle.Lib.HealthCheck.IOC
{
    /// <summary>
    /// Contains extension methods for adding custom health checks to a service collection.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds custom health checks to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add the health checks to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
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
            healthChecksBuilder.AddServicesCheck();
            healthChecksBuilder.AddRabbitMqCheck();

            return services;
        }

        /// <summary>
        /// Adds in-memory storage for health check results and configures webhooks and other settings.
        /// </summary>
        /// <param name="services">The service collection to add the in-memory storage to.</param>
        /// <returns>The modified service collection.</returns>
        public static IServiceCollection AddHealthChecksUIMemoryStorage(this IServiceCollection services)
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