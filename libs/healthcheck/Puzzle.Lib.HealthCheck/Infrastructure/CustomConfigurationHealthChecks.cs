namespace Puzzle.Lib.HealthCheck.Infrastructure;

/// <summary>
/// Contains extension methods for adding system configuration health checks to an <see cref="IHealthChecksBuilder"/>.
/// </summary>
public static class CustomConfigurationHealthChecks
{
    private static readonly string[] MEMORY_TAGS = ["Server", "Process", "Memory"];
    private static readonly string[] DISK_TAGS = ["Server", "Storage", "Capacity", "Disk"];
    private static readonly string[] REDIS_TAGS = ["Cache", "NoSQL", "Data Store", "In-Memory"];
    private static readonly string[] RABBITMQ_TAGS = ["RabbitMQ", "Event", "Message"];
    private static readonly string[] POSTGRE_TAGS = ["Postgre Server", "DbContext", "SQL", "Database", "NPG"];
    private static readonly string[] MICROSOFT_TAGS = ["Microsoft Server", "DbContext", "SQL", "Database", "MS"];
    private static readonly string[] HANGFIRE_TAGS = ["Schedule Task", "Cron", "Jobs"];

    /// <summary>
    /// Adds system configuration health checks for disk storage and process allocated memory to an <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> to add the health checks to.</param>
    /// <returns>The updated <see cref="IHealthChecksBuilder"/>.</returns>
    public static IHealthChecksBuilder AddSystemCheck(this IHealthChecksBuilder healthChecksBuilder)
    {
        healthChecksBuilder.AddDiskStorageHealthCheck(setup =>
        {
            setup.AddDrive(@"C:\", 2048);
        },
        name: "[System] - Storage/Disk Capacity",
        failureStatus: HealthStatus.Unhealthy,
        tags: DISK_TAGS);

        healthChecksBuilder.AddProcessAllocatedMemoryHealthCheck(
            name: "[Memory] - Allocated Memory Capacity",
            maximumMegabytesAllocated: 2048,
            tags: MEMORY_TAGS);

        return healthChecksBuilder;
    }

    /// <summary>
    /// Adds health checks for registered services to an <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> to add the health checks to.</param>
    /// <returns>The updated <see cref="IHealthChecksBuilder"/>.</returns>
    public static IHealthChecksBuilder AddServicesCheck(this IHealthChecksBuilder healthChecksBuilder, List<HealthChecksUrlGroupSetting> healthChecksUrlGroupSettings)
    {
        ArgumentNullException.ThrowIfNull(healthChecksUrlGroupSettings);

        foreach (var serviceInfo in healthChecksUrlGroupSettings)
        {
            healthChecksBuilder.AddUrlGroup(
                uri: serviceInfo.ServiceUri,
                name: serviceInfo.Name,
                failureStatus: HealthStatus.Unhealthy,
                tags: serviceInfo.Tags);
        }

        return healthChecksBuilder;
    }

    /// <summary>
    /// Adds a Redis connection health check to the IHealthChecksBuilder.
    /// </summary>
    /// <param name="healthChecksBuilder">The IHealthChecksBuilder to add the Redis connection health check to.</param>
    /// <param name="connectionString">The Redis connection string.</param>
    /// <returns>The updated IHealthChecksBuilder.</returns>
    /// <exception cref="ArgumentException">Thrown when the connection string is null or empty.</exception>
    public static IHealthChecksBuilder AddRedisCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        return healthChecksBuilder.AddRedis(
                   redisConnectionString: connectionString,
                   name: "[Redis] - Caching Database",
                   failureStatus: HealthStatus.Unhealthy,
                   tags: REDIS_TAGS);
    }

    /// <summary>
    /// Adds a health check for RabbitMQ message broker to the IHealthChecksBuilder.
    /// </summary>
    /// <param name="healthChecksBuilder">The IHealthChecksBuilder instance to add the health check to.</param>
    /// <returns>The IHealthChecksBuilder instance with the added RabbitMQ health check.</returns>
    public static IHealthChecksBuilder AddRabbitMqCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        healthChecksBuilder.AddRabbitMQ(
            rabbitConnectionString: connectionString,
            name: "[RabbitMQ] - Message Broker",
            failureStatus: HealthStatus.Unhealthy,
            tags: RABBITMQ_TAGS);

        return healthChecksBuilder;
    }

    /// <summary>
    /// Adds a health check for a PostgreSQL database using NpgSql to the specified <see cref="IHealthChecksBuilder"/>.
    /// </summary>
    /// <param name="healthChecksBuilder">The <see cref="IHealthChecksBuilder"/> to add the health check to.</param>
    /// <param name="connectionString">The connection string to the PostgreSQL database.</param>
    /// <returns>The <see cref="IHealthChecksBuilder"/> instance with the health check added.</returns>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="connectionString"/> is null or empty.</exception>
    public static IHealthChecksBuilder AddNpgSqlDatabaseCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        return healthChecksBuilder.AddNpgSql(
                connectionString: connectionString,
                name: "[NPG SQL] - PostgreSQL Database",
                failureStatus: HealthStatus.Unhealthy,
                tags: POSTGRE_TAGS);
    }

    /// <summary>
    /// Adds a Microsoft SQL Server (MsSQL) database health check to the specified health checks builder.
    /// </summary>
    /// <param name="healthChecksBuilder">The health checks builder to add the MsSQL database health check to.</param>
    /// <param name="connectionString">The connection string for the MsSQL database.</param>
    /// <returns>The health checks builder with the added MsSQL database health check.</returns>
    /// <exception cref="ArgumentException">Thrown when the connection string is null or empty.</exception>
    public static IHealthChecksBuilder AddMsSqlDatabaseCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString);

        return healthChecksBuilder.AddSqlServer(
                connectionString: connectionString,
                name: "[MS SQL] - MsSQL Database",
                failureStatus: HealthStatus.Unhealthy,
                tags: MICROSOFT_TAGS);
    }

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
        tags: HANGFIRE_TAGS,
        timeout: TimeSpan.FromSeconds(30));

        return healthChecksBuilder;
    }
}
