namespace Puzzle.Lib.Hangfire.IOC
{
    /// <summary>
    /// Extension methods for <see cref="IServiceCollection"/> to configure and add Hangfire with SqlServer storage.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds Hangfire services with SqlServer storage.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add services to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddHangfireWithSqlServerStorage(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            IConfigurationSection configurationSection = configuration.GetRequiredSection(nameof(HangfireSetting));
            services.Configure<HangfireSetting>(configurationSection);
            HangfireSetting hangfireSetting = configurationSection.Get<HangfireSetting>();

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(
                    nameOrConnectionString: hangfireSetting.ConnectionString,
                    options: GenerateSqlServerStorageOptions(hangfireSetting)
                    ).WithJobExpirationTimeout(TimeSpan.FromDays(hangfireSetting.JobExpirationTimeoutDay));
            });

            services.AddHangfireServer();

            return services;
        }

        /// <summary>
        /// Generates SqlServerStorage options based on the provided HangfireSetting object.
        /// </summary>
        /// <param name="hangfireSetting">The HangfireSetting object to use for generating options.</param>
        /// <returns>A <see cref="SqlServerStorageOptions"/> object generated using the provided HangfireSetting object.</returns>
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
