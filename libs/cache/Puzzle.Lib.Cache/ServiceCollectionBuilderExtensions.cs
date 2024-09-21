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

        ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(new ConfigurationOptions
        {
            EndPoints = { redisServerSetting.ConnectionString },
            AbortOnConnectFail = redisServerSetting.AbortOnConnectFail,
            AsyncTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.AsyncTimeOutSecond).TotalMilliseconds,
            ConnectTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.ConnectTimeOutSecond).TotalMilliseconds,
            User = redisServerSetting.Username,
            Password = redisServerSetting.Password,
            DefaultDatabase = redisServerSetting.DefaultDatabase,
            AllowAdmin = redisServerSetting.AllowAdmin,
            ChannelPrefix = new RedisChannel($"{Assembly.GetEntryAssembly().GetName().Name.ToLowerInvariant()}:", RedisChannel.PatternMode.Auto)            
        });

        services.TryAddSingleton<IConnectionMultiplexer>(provider =>
        {
            RedisPolicies.Logger = provider.GetRequiredService<ILogger<StackExchangeRedisManager>>();

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