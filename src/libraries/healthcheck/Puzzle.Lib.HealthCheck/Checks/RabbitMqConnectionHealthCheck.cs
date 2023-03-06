namespace Puzzle.Lib.HealthCheck.Checks
{
    internal static class RabbitMqConnectionHealthCheck
    {
        internal static IHealthChecksBuilder AddRabbitMqCheck(this IHealthChecksBuilder healthChecksBuilder)
        {
            healthChecksBuilder.AddRabbitMQ(
                name: "[RabbitMQ] - Message Broker",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "RabbitMQ", "Event", "Message" });

            return healthChecksBuilder;
        }
    }
}
