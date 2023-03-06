namespace Puzzle.Lib.Hangfire.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add hangfire with sql server storage
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://www.hangfire.io/"/>
        /// <returns>type of built-in service collection interface</returns>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static IServiceCollection AddHangfireWithSqlServerStorage(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            HangfireSetting hangfireSetting = default;
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<HangfireSetting>(configuration.GetRequiredSection(nameof(HangfireSetting)));
            services.TryAddSingleton<IHangfireSetting>(provider =>
            {
                hangfireSetting = provider.GetRequiredService<IOptions<HangfireSetting>>().Value;
                return hangfireSetting;
            });

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(hangfireSetting.ConnectionString, GenerateSqlServerStorageOptions(hangfireSetting))
                                  .WithJobExpirationTimeout(TimeSpan.FromDays(hangfireSetting.JobExpirationTimeoutDay));
            });

            services.AddHangfireServer();

            return services;
        }

        private static SqlServerStorageOptions GenerateSqlServerStorageOptions(HangfireSetting hangfireSetting)
        {
            SqlServerStorageOptions sqlServerStorageOptions = new()
            {
                PrepareSchemaIfNecessary = true,
                QueuePollInterval = TimeSpan.FromMinutes(hangfireSetting.QueuePollIntervalMinute),
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(hangfireSetting.CommandBatchMaxTimeoutMinute)
            };

            return sqlServerStorageOptions;
        }
    }
}
