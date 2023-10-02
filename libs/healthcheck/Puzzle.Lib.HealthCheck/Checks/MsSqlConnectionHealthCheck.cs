namespace Puzzle.Lib.HealthCheck.Checks;

/// <summary>
/// A class that provides methods for adding Microsoft SQL Server (MsSQL) database health checks.
/// </summary>
public static class MsSqlConnectionHealthCheck
{
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
                tags: new[] { "Microsoft Server", "DbContext", "SQL", "Database", "MS" });
    }
}
