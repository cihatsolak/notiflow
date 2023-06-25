namespace Puzzle.Lib.HealthCheck.Checks;

/// <summary>
/// Contains extension methods for adding system configuration health checks to an <see cref="IHealthChecksBuilder"/>.
/// </summary>
public static class SystemConfigurationHealthCheck
{
    /// <summary>
    /// Adds system configuration health checks for disk storage and process allocated memory to an <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> to add the health checks to.</param>
    /// <returns>The updated <see cref="IHealthChecksBuilder"/>.</returns>
    public static IHealthChecksBuilder AddSystemConfigurationCheck(this IHealthChecksBuilder healthChecksBuilder)
    {
        healthChecksBuilder.AddDiskStorageHealthCheck(setup =>
        {
            setup.AddDrive(@"C:\", 2048);

        },
        name: "[Server] - Disk Capacity",
        failureStatus: HealthStatus.Unhealthy,
        tags: new[] { "Server", "Storage", "Capacity" });

        healthChecksBuilder.AddProcessAllocatedMemoryHealthCheck(
            name: "[Memory] - Allocated Memory Capacity",
            maximumMegabytesAllocated: 2048,
            tags: new[] { "Server", "Process", "Memory" });

        return healthChecksBuilder;
    }
}
