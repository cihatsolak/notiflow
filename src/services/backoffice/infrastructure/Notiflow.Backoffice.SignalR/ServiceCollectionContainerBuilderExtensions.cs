namespace Notiflow.Backoffice.SignalR;

public static class ServiceCollectionContainerBuilderExtensions
{
    private static readonly ILogger<NotificationHub> _logger = null;

    public static IServiceCollection AddSignalConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        RedisServerSetting redisServerSetting = configuration.GetRequiredSection(nameof(RedisServerSetting)).Get<RedisServerSetting>();

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

        services.AddSignalR(hubOptions =>
        {
            //detaylı hata mesajlarının istemcilere gönderilip gönderilmeyeceğini belirler. Detaylı hata mesajları, sunucuda oluşan istisna detaylarını içerir.
            hubOptions.EnableDetailedErrors = false;

            //istemcilerin bağlantı başlatma işlemi sırasında sunucuya karşı zaman aşımına uğramasına izin verilen süreyi belirler. Varsayılan değeri 15 saniyedir.
            hubOptions.HandshakeTimeout = TimeSpan.FromSeconds(30);

            //client, bir mesaj gönderme işlemi başlatmadan 25 saniye boyunca herhangi bir eylemde bulunmazsa, bağlantı otomatik olarak kapanır.
            hubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(25);

            //sunucunun bağlı istemcilere periyodik olarak "keep-alive" ping'leri gönderme aralığını belirler. Varsayılan aralık 15 saniyedir.
            hubOptions.KeepAliveInterval = TimeSpan.FromSeconds(20);
        })
        .AddStackExchangeRedis(redisOptions =>
        {
            redisOptions.Configuration = config;

            redisOptions.ConnectionFactory = async writer =>
            {
                config.EndPoints.Add(redisServerSetting.ConnectionString);
                
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

        services.AddScoped<IHubDispatcher, HubDispatcher>();

        return services;
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
