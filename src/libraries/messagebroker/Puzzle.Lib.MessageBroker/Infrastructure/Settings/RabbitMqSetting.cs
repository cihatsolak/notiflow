namespace Puzzle.Lib.MessageBroker.Infrastructure.Settings
{
    public interface IRabbitMqSetting
    {
        string HostName { get; init; }
        string Username { get; init; }
        string Password { get; init; }
    }

    internal sealed record RabbitMqSetting : IRabbitMqSetting
    {
        public string HostName { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
