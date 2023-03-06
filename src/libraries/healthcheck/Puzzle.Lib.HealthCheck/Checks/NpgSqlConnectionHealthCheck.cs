namespace Puzzle.Lib.HealthCheck.Checks
{
    internal static class NpgSqlConnectionHealthCheck
    {
        internal static IHealthChecksBuilder AddNpgSqlDatabaseCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString);

            healthChecksBuilder.AddNpgSql(
                    npgsqlConnectionString: connectionString,
                    name: "[NPG SQL] - Postgresql Database",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "Postgre Server", "DbContext", "SQL" });

            return healthChecksBuilder;
        }
    }
}
