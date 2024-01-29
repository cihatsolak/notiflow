namespace Puzzle.Lib.Hangfire;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/> to configure and add Hangfire with SqlServer storage.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds Hangfire services with microsoft sql server storage.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IServiceCollection AddHangfireMsSql(this IServiceCollection services, Action<HangfireSetting> configure)
    {
        HangfireSetting hangfireSetting = new();
        configure?.Invoke(hangfireSetting);

        services.Configure(configure);

        services.AddHangfire((provider, options) =>
        {
            options.UseSqlServerStorage(hangfireSetting.ConnectionString, new()
            {
                PrepareSchemaIfNecessary = true,
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(8),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.FromSeconds(30),
                DisableGlobalLocks = true,
                UseRecommendedIsolationLevel = true
            }).WithJobExpirationTimeout(TimeSpan.FromDays(10));

            options.UseHeartbeatPage(TimeSpan.FromMinutes(1));

            options.UseFilter(new AutomaticRetryAttribute() { Attempts = hangfireSetting.GlobalAutomaticRetryAttempts });

            options.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            options.UseSimpleAssemblyNameTypeSerializer();
            options.UseRecommendedSerializerSettings();
            options.UseSerilogLogProvider();
            options.UseColouredConsoleLogProvider();
            options.UseDefaultCulture(CultureInfo.CurrentCulture, CultureInfo.CurrentCulture);
            
            options.UseDashboardMetric(DashboardMetrics.ServerCount)
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

        services.AddHangfireServer(options =>
        {
            options.ServerName = string.Format("{0}.{1}", Environment.MachineName, Random.Shared.Next(100000, 999999));
        });

        return services;
    }
}
