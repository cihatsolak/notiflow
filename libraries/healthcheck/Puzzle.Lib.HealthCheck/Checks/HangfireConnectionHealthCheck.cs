namespace Puzzle.Lib.HealthCheck.Checks
{
    /// <summary>
    /// Contains extension method to add Hangfire health check to the HealthChecksBuilder.
    /// </summary>
    public static class HangfireConnectionHealthCheck
    {
        /// <summary>
        /// Adds a Hangfire health check to the specified HealthChecksBuilder.
        /// </summary>
        /// <param name="healthChecksBuilder">The HealthChecksBuilder to add the Hangfire check to.</param>
        /// <returns>The same HealthChecksBuilder instance with the Hangfire check added.</returns>
        public static IHealthChecksBuilder AddHangfireCheck(this IHealthChecksBuilder healthChecksBuilder)
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
