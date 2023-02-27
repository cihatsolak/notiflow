namespace Notiflow.Lib.HealthCheck.Checks
{
    internal static class ServiceConnectionHealthCheck
    {
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
