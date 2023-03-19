namespace Puzzle.Lib.HealthCheck.Checks
{
    /// <summary>
    /// A class containing a method to add health checks for a group of services with URLs.
    /// </summary>
    internal static class ServiceConnectionHealthCheck
    {
        /// <summary>
        /// Adds health checks for a group of services with URLs.
        /// </summary>
        /// <param name="healthChecksBuilder">The IHealthChecksBuilder instance to add the health checks to.</param>
        /// <param name="healthChecksUrlGroupSetting">The settings for the group of services to add health checks for.</param>
        /// <returns>The IHealthChecksBuilder instance.</returns>
        internal static IHealthChecksBuilder AddServicesCheck(this IHealthChecksBuilder healthChecksBuilder, IHealthChecksUrlGroupSetting healthChecksUrlGroupSetting)
        {
            foreach (var serviceInfo in healthChecksUrlGroupSetting.UrlGroups)
            {
                healthChecksBuilder.AddUrlGroup(
                    uri: serviceInfo.ServiceUri,
                    name: serviceInfo.Name,
                    failureStatus: HealthStatus.Unhealthy,
                    tags: serviceInfo.Tags);
            }

            return healthChecksBuilder;
        }
    }
}
