namespace Puzzle.Lib.HealthCheck.Checks
{
    internal static class RedisConnectionHealthCheck
    {
        internal static IHealthChecksBuilder AddRedisCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
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
}
