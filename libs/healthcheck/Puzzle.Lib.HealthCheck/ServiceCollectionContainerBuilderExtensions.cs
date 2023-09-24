﻿namespace Puzzle.Lib.HealthCheck;

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
    public static IServiceCollection AddHealthChecksWithDependencies(this IServiceCollection services)
    {
        IHealthChecksBuilder healthChecksBuilder = services.AddHealthChecks();
        ArgumentNullException.ThrowIfNull(healthChecksBuilder);

        healthChecksBuilder.AddHangfireCheck();
        healthChecksBuilder.AddRedisCheck("redis connection string");
        healthChecksBuilder.AddNpgSqlDatabaseCheck("npg sql database connection string");
        healthChecksBuilder.AddSystemCheck();
        healthChecksBuilder.AddServicesCheck(Array.Empty<HealthChecksUrlGroupSetting>().ToList());
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
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(HealthWebHookSetting));
        HealthWebHookSetting healthWebHookSetting = configurationSection.Get<HealthWebHookSetting>();

        services.AddHealthChecksUI(setupSettings =>
        {
            setupSettings.SetEvaluationTimeInSeconds(60);
            setupSettings.SetApiMaxActiveRequests(150);
            setupSettings.MaximumHistoryEntriesPerEndpoint(5000);
            setupSettings.AddWebhookNotification(
                name: healthWebHookSetting.Name,
                uri: healthWebHookSetting.Uri,
                payload: healthWebHookSetting.Payload,
                restorePayload: healthWebHookSetting.RestorePayload
                );
        }).AddInMemoryStorage();

        return services;
    }
}