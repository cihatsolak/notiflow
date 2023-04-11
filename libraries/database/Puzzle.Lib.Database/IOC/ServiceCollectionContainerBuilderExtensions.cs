namespace Puzzle.Lib.Database.IOC
{
    /// <summary>
    /// Extension methods for setting up MVC services in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
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

            var webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            services.AddDbContextPool<TDbContext>(contextOptions =>
            {
                ConfigureWarnings(contextOptions);
                ConfigureLog(contextOptions, webHostEnvironment);

                contextOptions.UseLazyLoadingProxies(false);
                contextOptions.UseNpgsql(configuration.GetConnectionString(contextName), sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(TDbContext).Assembly.FullName);
                    sqlOptions.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);


                    if (isSplitQuery)
                    {
                        sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    }
                });
            });

            ConfigureException(services, webHostEnvironment);

            return services;
        }

        /// <summary>
        /// Entity repository
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection</returns>
        [Obsolete("It is suitable for use in entity framework 6 and lower versions.")]
        public static IServiceCollection AddEfEntityRepository(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IEf6EntityRepository<>), typeof(Ef6EntityRepository<>));
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

        private static void ConfigureLog(DbContextOptionsBuilder contextOptions, IWebHostEnvironment webHostEnvironment)
        {
            contextOptions.EnableDetailedErrors(!webHostEnvironment.IsProduction());
            contextOptions.EnableSensitiveDataLogging(!webHostEnvironment.IsProduction());
            contextOptions.LogTo(Console.WriteLine, LogLevel.Warning);
            contextOptions.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
        }

        private static void ConfigureException(IServiceCollection services, IWebHostEnvironment webHostEnvironment)
        {
            if (!webHostEnvironment.IsProduction())
            {
                services.AddDatabaseDeveloperPageExceptionFilter();
            }
        }
    }
}