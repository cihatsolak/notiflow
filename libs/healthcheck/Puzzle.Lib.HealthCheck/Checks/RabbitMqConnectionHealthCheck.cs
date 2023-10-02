namespace Puzzle.Lib.HealthCheck.Checks;

/// <summary>
/// Provides a method to add a health check for RabbitMQ message broker to the IHealthChecksBuilder.
/// </summary>
public static class RabbitMqConnectionHealthCheck
{
    /// <summary>
    /// Adds a health check for RabbitMQ message broker to the IHealthChecksBuilder.
    /// </summary>
    /// <param name="healthChecksBuilder">The IHealthChecksBuilder instance to add the health check to.</param>
    /// <returns>The IHealthChecksBuilder instance with the added RabbitMQ health check.</returns>
    public static IHealthChecksBuilder AddRabbitMqCheck(this IHealthChecksBuilder healthChecksBuilder, string connectionString)
    {
        healthChecksBuilder.AddRabbitMQ(
            rabbitConnectionString: connectionString,
            name: "[RabbitMQ] - Message Broker",
            failureStatus: HealthStatus.Unhealthy,
            tags: new[] { "RabbitMQ", "Event", "Message" });

        return healthChecksBuilder;
    }
}
