namespace Notiflow.Common.Settings;

public sealed record RabbitMqSetting
{
    public string ConnectionString { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}
