namespace Notiflow.Lib.Database.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        private static IWebHostEnvironment WebHostEnvironment { get; set; }
        private static bool NotLiveEnvironemnt => !WebHostEnvironment.IsProduction() && !WebHostEnvironment.IsStaging();

        /// <summary>
        /// Add postgre sql server db context
        /// </summary>
        /// <typeparam name="TDbContext">type of db dbcontext</typeparam>
        /// <param name="services">type of built-in service collection interface</param>
        /// <param name="contextName">database context class name</param>
        /// <param name="isSplitQuery">split query behavior</param>
        /// <param name="enabledLazyLoading">enabled lazy loading</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/"/>
        /// <returns>type of built-in service collection</returns>
        /// <exception cref="ArgumentNullException">when the service provider cannot be built</exception>
        /// <exception cref="InvalidOperationException">If the container cannot invoke the configuration interface</exception>
        public static IServiceCollection AddPostgreSql<TDbContext>(this IServiceCollection services, string contextName, bool isSplitQuery = true) where TDbContext : DbContext
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            WebHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContextPool<TDbContext>(contextOptions =>
            {
                ConfigureWarnings(contextOptions);
                ConfigureLog(contextOptions);

                contextOptions.UseLazyLoadingProxies(false);
                contextOptions.UseNpgsql(configuration.GetConnectionString(contextName), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                    sqlOptions.CommandTimeout(Convert.ToInt16(TimeSpan.FromSeconds(60).TotalSeconds));

                    if (isSplitQuery)
                    {
                        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }
                });
            });

            ConfigureException(services);
            AddEfEntityRepository(services);

            return services;
        }

        private static void ConfigureWarnings(DbContextOptionsBuilder contextOptions)
        {
            contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning));
            contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));
            contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning));
            contextOptions.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.BoolWithDefaultWarning));
            contextOptions.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
        }

        private static void ConfigureLog(DbContextOptionsBuilder contextOptions)
        {
            contextOptions.LogTo(Log.Logger.Warning, LogLevel.Warning);
            contextOptions.EnableSensitiveDataLogging(NotLiveEnvironemnt);
            contextOptions.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                if (NotLiveEnvironemnt)
                {
                    builder.AddConsole();
                }
            }));
        }

        private static void ConfigureException(IServiceCollection services)
        {
            if (NotLiveEnvironemnt)
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }
        }

        private static void AddEfEntityRepository(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IEfEntityRepository<>), typeof(EfEntityRepository<>));
            services.TryAddScoped<IBaseUnitOfWork, BaseUnitOfWork>();
        }
    }
}
