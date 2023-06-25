namespace Puzzle.Lib.HealthCheck.Checks;

/// <summary>
/// Provides extension methods to add Redis connection health checks to the IHealthChecksBuilder.
/// </summary>
public static class RedisConnectionHealthCheck
{
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

        healthChecksBuilder.AddRedis(
               redisConnectionString: connectionString,
               name: "[Redis] - Cache Database",
               failureStatus: HealthStatus.Unhealthy,
               tags: new[] { "Cache", "NOSQL", "Database" });

        return healthChecksBuilder;
    }
}
