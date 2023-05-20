﻿namespace Puzzle.Lib.Cache.IOC
{
    /// <summary>
    /// Contains extension methods to register Redis related services to the <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds Redis related services to the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>The same instance of the <see cref="IServiceCollection"/> for chaining.</returns>
        public static IServiceCollection AddRedisService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(RedisServerSetting));
            services.Configure<RedisServerSetting>(configurationSection);
            RedisServerSetting redisServerSetting = configurationSection.Get<RedisServerSetting>();

            services.TryAddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { redisServerSetting.ConnectionString },
                AbortOnConnectFail = redisServerSetting.AbortOnConnectFail,
                AsyncTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.AsyncTimeOutSecond).TotalMilliseconds,
                ConnectTimeout = (int)TimeSpan.FromSeconds(redisServerSetting.ConnectTimeOutSecond).TotalMilliseconds,
                User = redisServerSetting.Username,
                Password = redisServerSetting.Password,
                DefaultDatabase = redisServerSetting.DefaultDatabase,
                AllowAdmin = redisServerSetting.AllowAdmin
            }));

            services.TryAddSingleton(provider =>
            {
                IConnectionMultiplexer connectionMultiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                return connectionMultiplexer.GetServer(redisServerSetting.ConnectionString);
            });

            services.TryAddSingleton<IRedisService>(provider =>
            {
                IConnectionMultiplexer connectionMultiplexer = provider.GetRequiredService<IConnectionMultiplexer>();
                IDatabase database = connectionMultiplexer.GetDatabase(redisServerSetting.DefaultDatabase);
                IServer server = connectionMultiplexer.GetServer(redisServerSetting.ConnectionString);
                ILogger<StackExchangeRedisManager> logger = provider.GetRequiredService<ILogger<StackExchangeRedisManager>>();

                return new StackExchangeRedisManager(database, server, logger, redisServerSetting.DefaultDatabase);
            });

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
}