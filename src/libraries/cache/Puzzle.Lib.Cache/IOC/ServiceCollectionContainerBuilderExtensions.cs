namespace Puzzle.Lib.Cache.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add redis service
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://redis.io/"/>
        /// <returns>type of built-in service collection interface</returns>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddRedisService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            RedisServerSetting redisServerSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<RedisServerSetting>(configuration.GetRequiredSection(nameof(RedisServerSetting)));
            services.TryAddSingleton<IRedisServerSetting>(provider =>
            {
                redisServerSetting = provider.GetRequiredService<IOptions<RedisServerSetting>>().Value;
                return redisServerSetting;
            });

            services.TryAddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { redisServerSetting.ConnectionString },
                AbortOnConnectFail = redisServerSetting.AbortOnConnectFail,
                AsyncTimeout = redisServerSetting.AsyncTimeOutMilliSecond,
                ConnectTimeout = redisServerSetting.ConnectTimeOutMilliSecond,
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

                return new RedisApiManager(database, server, redisServerSetting.DefaultDatabase);
            });

            return services;
        }
    }
}