namespace Puzzle.Lib.HealthCheck.Checks
{
    internal static class HangfireConnectionHealthCheck
    {
        internal static IHealthChecksBuilder AddHangfireCheck(this IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder.AddHangfire(setup =>
            {
                setup.MaximumJobsFailed = 3;
                setup.MinimumAvailableServers = 1;
            },
            name: "[Hangfire] - Scheduled Jobs",
            failureStatus: HealthStatus.Unhealthy,
            tags: new[] { "Schedule Task", "Cron", "Jobs" });

            return healthChecksBuilder;
        }
    }
}
