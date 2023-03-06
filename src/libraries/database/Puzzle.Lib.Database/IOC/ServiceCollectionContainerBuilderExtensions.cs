namespace Puzzle.Lib.Database.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        private static IWebHostEnvironment WebHostEnvironment { get; set; }

        /// <summary>
        /// Add postgresql provider as database context
        /// </summary>
        /// <typeparam name="TDbContext">type of db dbcontext</typeparam>
        /// <param name="services">type of built-in service collection interface</param>
        /// <param name="contextName">database context class name</param>
        /// <param name="isSplitQuery">split query behavior</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/ef/core/dbcontext-configuration/"/>
        /// <returns>type of built-in service collection</returns>
        /// <exception cref="ArgumentNullException">when the service provider cannot be built</exception>
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

            return services;
        }

        /// <summary>
        /// Entity repository
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection</returns>
        public static IServiceCollection AddEfEntityRepository(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IEfEntityRepository<>), typeof(EfEntityRepository<>));
            services.TryAddScoped<IBaseUnitOfWork, BaseUnitOfWork>();

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
            contextOptions.EnableSensitiveDataLogging(WebHostEnvironment.IsDevEnvironment());
            contextOptions.UseLoggerFactory(LoggerFactory.Create(builder =>
            {
                if (WebHostEnvironment.IsDevEnvironment())
                {
                    builder.AddConsole();
                }
            }));
        }

        private static void ConfigureException(IServiceCollection services)
        {
            if (WebHostEnvironment.IsDevEnvironment())
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }
        }
    }
}