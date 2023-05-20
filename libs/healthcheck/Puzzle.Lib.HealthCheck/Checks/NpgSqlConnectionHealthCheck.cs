namespace Puzzle.Lib.HealthCheck.Checks
{
    /// <summary>
    /// Provides a set of methods to add a health check for a PostgreSQL database using NpgSql.
    /// </summary>
    public static class NpgSqlConnectionHealthCheck
    {
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

            healthChecksBuilder.AddNpgSql(
                    npgsqlConnectionString: connectionString,
                    name: "[NPG SQL] - Postgresql Database",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new[] { "Postgre Server", "DbContext", "SQL" });

            return healthChecksBuilder;
        }
    }
}
