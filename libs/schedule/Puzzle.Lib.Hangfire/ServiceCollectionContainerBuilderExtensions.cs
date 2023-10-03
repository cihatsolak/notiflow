namespace Puzzle.Lib.Hangfire;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to configure and add Hangfire with SqlServer storage.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds Hangfire services with SqlServer storage.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddHangfireWithSqlServerStorage(this IServiceCollection services, Action<HangfireSetting> configure)
    {
        HangfireSetting hangfireSetting = new();
        configure?.Invoke(hangfireSetting);

        services.Configure(configure);

        services.AddHangfire(config =>
        {
            config.UseSqlServerStorage(hangfireSetting.ConnectionString, new()
            {
                PrepareSchemaIfNecessary = true,
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(8),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(30),
                DisableGlobalLocks = true,
                UseRecommendedIsolationLevel = true
            })
           .WithJobExpirationTimeout(TimeSpan.FromDays(15));

            config.UseDashboardMetric(DashboardMetrics.ServerCount)
                  .UseDashboardMetric(SqlServerStorage.ActiveConnections)
                  .UseDashboardMetric(SqlServerStorage.TotalConnections)
                  .UseDashboardMetric(DashboardMetrics.RecurringJobCount)
                  .UseDashboardMetric(DashboardMetrics.RetriesCount)
                  .UseDashboardMetric(DashboardMetrics.AwaitingCount)
                  .UseDashboardMetric(DashboardMetrics.EnqueuedAndQueueCount)
                  .UseDashboardMetric(DashboardMetrics.ScheduledCount)
                  .UseDashboardMetric(DashboardMetrics.ProcessingCount)
                  .UseDashboardMetric(DashboardMetrics.SucceededCount)
                  .UseDashboardMetric(DashboardMetrics.FailedCount)
                  .UseDashboardMetric(DashboardMetrics.DeletedCount)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UseSerilogLogProvider();
        });

        services.AddHangfireServer();

        return services;
    }
}
