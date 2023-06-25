namespace Puzzle.Lib.HealthCheck.Checks;

/// <summary>
/// A class containing a method to add health checks for a group of services with URLs.
/// </summary>
public static class ServiceConnectionHealthCheck
{
    /// <summary>
    /// Adds health checks for registered services to an <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> to add the health checks to.</param>
    /// <returns>The updated <see cref="IHealthChecksBuilder"/>.</returns>
    public static IHealthChecksBuilder AddServicesCheck(this IHealthChecksBuilder healthChecksBuilder)
    {
        IServiceProvider serviceProvider = healthChecksBuilder.Services.BuildServiceProvider();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(HealthChecksUrlGroupSetting));
        HealthChecksUrlGroupSetting healthChecksUrlGroupSetting = configurationSection.Get<HealthChecksUrlGroupSetting>();

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
