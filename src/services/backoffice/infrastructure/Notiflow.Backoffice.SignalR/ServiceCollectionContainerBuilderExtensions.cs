namespace Notiflow.Backoffice.SignalR;

public static class ServiceCollectionContainerBuilderExtensions
{
    private static readonly ILogger<NotificationHub> _logger = null;

    public static IServiceCollection AddSignalConfiguration(this WebApplicationBuilder builder)
    {

        RedisServerSetting redisServerSetting = builder.Configuration.GetRequiredSection(nameof(RedisServerSetting)).Get<RedisServerSetting>();

        ConfigurationOptions config = new()
        {
            EndPoints = { redisServerSetting.ConnectionString },
            AbortOnConnectFail = redisServerSetting.AbortOnConnectFail,
            AsyncTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.AsyncTimeOutSecond).TotalMilliseconds,
            ConnectTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.ConnectTimeOutSecond).TotalMilliseconds,
            User = redisServerSetting.Username,
            Password = redisServerSetting.Password,
            DefaultDatabase = redisServerSetting.DefaultDatabase,
            AllowAdmin = redisServerSetting.AllowAdmin
        };

        builder.Services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
            hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(3);
            hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(3);
        })

        .AddStackExchangeRedis(redisOptions =>
        {
            redisOptions.Configuration = config;

            redisOptions.ConnectionFactory = async writer =>
            {
                config.EndPoints.Add(IPAddress.Loopback, 0);
                config.SetDefaultPorts();

                var connection = await ConnectionMultiplexer.ConnectAsync(config, writer);
                
                connection.ConnectionFailed += Connection_ConnectionFailed;
                connection.ConnectionRestored += Connection_ConnectionRestored;

                if (!connection.IsConnected)
                {
                    _logger.LogCritical("Did not connect to Redis.");
                }

                return connection;
            };
        });

        builder.Services.AddScoped<IHubDispatcher, HubDispatcher>();

        return builder.Services;
    }

    private static void Connection_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
    {
        _logger.LogCritical(e.Exception, "Connection to redis failed. Failure Type: {FailureType}", e.FailureType);
    }

    private static void Connection_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
    {
        _logger.LogCritical(e.Exception, "Connected and reinstalled.. Failure Type: {FailureType}", e.FailureType);
    }
}
