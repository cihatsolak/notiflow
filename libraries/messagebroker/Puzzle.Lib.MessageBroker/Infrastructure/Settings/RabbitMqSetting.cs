namespace Puzzle.Lib.MessageBroker.Infrastructure.Settings
{
    /// <summary>
    /// Represents the settings for a RabbitMQ server connection.
    /// </summary>
    public sealed record RabbitMqSetting
    {
        /// <summary>
        /// Gets or sets the host name of the RabbitMQ server.
        /// </summary>
        public required string HostName { get; init; }

        /// <summary>
        /// Gets or sets the username for authenticating with the RabbitMQ server.
        /// </summary>
        public required string Username { get; init; }

        /// <summary>
        /// Gets or sets the password for authenticating with the RabbitMQ server.
        /// </summary>
        public required string Password { get; init; }
    }
}
