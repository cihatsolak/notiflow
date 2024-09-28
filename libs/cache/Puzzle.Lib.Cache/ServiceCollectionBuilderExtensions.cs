namespace Puzzle.Lib.Cache;

/// <summary>
/// Contains extension methods to register Redis related services to the <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionBuilderExtensions
{
    /// <summary>
    /// Adds Redis related services to the <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
    /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection AddRedisService(this IServiceCollection services, Action<RedisServerSetting> configure)
    {
        RedisServerSetting redisServerSetting = new();
        configure?.Invoke(redisServerSetting);

        services.Configure(configure);

        string entryAssembly = Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant();

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            // Redis sunucusunun bağlantı noktası.
            EndPoints = { redisServerSetting.ConnectionString },

            // Bağlantı kurulamadığında bağlantının hemen kesilip kesilmeyeceğini belirler.
            AbortOnConnectFail = redisServerSetting.AbortOnConnectFail,

            // Asenkron işlemler için zaman aşımı süresi.
            AsyncTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.AsyncTimeOutSecond).TotalMilliseconds,

            // Sunucuya bağlantı için bekleme süresi.
            ConnectTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.ConnectTimeOutSecond).TotalMilliseconds,

            // Redis sunucusuna erişim için kullanıcı adı.
            User = redisServerSetting.Username,

            // Redis sunucusuna erişim için şifre bilgisi.
            Password = redisServerSetting.Password,

            // Bağlantı açıldığında varsayılan olarak hangi veritabanına bağlanılacağını belirler.
            DefaultDatabase = redisServerSetting.DefaultDatabase,

            // Redis sunucusunda yönetimsel komutları kullanma izni.
            AllowAdmin = redisServerSetting.AllowAdmin,

            // Redis ile yayınla/abone ol işlemleri sırasında kullanılan kanal adlarının ön ekidir.
            ChannelPrefix = new RedisChannel($"entryAssembly}:", RedisChannel.PatternMode.Auto),

            // Uygulamanızın Redis sunucusundaki bağlantıları tanımlamak için bir isim belirler.
            ClientName = entryAssembly
        });


        services.TryAddSingleton<IConnectionMultiplexer>(provider =>
        {
            RedisPolicies.Logger = provider.GetRequiredService<ILogger<StackExchangeRedisManager>>();
            RedisPolicies.RetryCount = redisServerSetting.RetryCount;

            return connectionMultiplexer;
        });

        services.TryAddSingleton(connectionMultiplexer.GetServer(redisServerSetting.ConnectionString));
        services.TryAddSingleton<IRedisService, StackExchangeRedisManager>();

        return services;
    }

    /// <summary>
    /// Adds a Redis messaging service to the specified service collection.
    /// </summary>
    /// <param name="services">The service collection to add the Redis messaging service to.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddRedisMessagingService(this IServiceCollection services)
    {
        services.TryAddSingleton<IRedisPublisherService, RedisPublisherManager>();

        return services;
    }
}