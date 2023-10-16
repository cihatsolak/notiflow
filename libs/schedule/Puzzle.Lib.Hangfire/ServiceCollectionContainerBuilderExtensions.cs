using Hangfire.Heartbeat;

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

        services.AddHangfire((provider, config) =>
        {
            config.UseSqlServerStorage(hangfireSetting.ConnectionString, new()
            {
                PrepareSchemaIfNecessary = true,
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(8),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(30),
                DisableGlobalLocks = true,
                UseRecommendedIsolationLevel = true
            }).WithJobExpirationTimeout(TimeSpan.FromDays(10));

            config.UseHeartbeatPage(TimeSpan.FromMinutes(1));

            config.UseFilter(new AutomaticRetryAttribute() { Attempts = hangfireSetting.GlobalAutomaticRetryAttempts });

            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();
            config.UseSerilogLogProvider();
            config.UseColouredConsoleLogProvider();

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
                  .UseDashboardMetric(DashboardMetrics.DeletedCount);

        });

        services.AddHangfireServer(opt =>
        {
            opt.ServerName = string.Format("{0}.{1}", Environment.MachineName, Guid.NewGuid().ToString());
        });

        return services;
    }
}
