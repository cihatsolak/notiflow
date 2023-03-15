namespace Puzzle.Lib.Hangfire.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
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
