using Puzzle.Lib.Database.Interceptors;

namespace Puzzle.Lib.Database.IOC;

/// <summary>
/// Provides extension methods for configuring SQL database in IServiceCollection.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds a PostgreSQL database to the service collection for the specified DbContext type and configuration key.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="services">The IServiceCollection instance to add the PostgreSQL database to.</param>
    /// <param name="configKey">The configuration key for the PostgreSQL database settings.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
    public static IServiceCollection AddPostgreSql<TDbContext>(this IServiceCollection services, string configKey) where TDbContext : DbContext
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        bool isProductionEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>().IsProduction();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        SqlSetting sqlSetting = configuration.GetRequiredSection(configKey).Get<SqlSetting>();

        services.AddDbContext<TDbContext>(contextOptions =>
        {
            contextOptions.ConfigureCustomWarnings();
            contextOptions.ConfigureCustomLogs(isProductionEnvironment);
            contextOptions.UseSnakeCaseNamingConvention();
           
            contextOptions.UseNpgsql(sqlSetting.ConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(TDbContext)).FullName);
                sqlOptions.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);

                if (sqlSetting.IsSplitQuery)
                {
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            });

            contextOptions.UseLazyLoadingProxies(false);
            contextOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

            contextOptions.AddInterceptors(new SlowQueryInterceptor(serviceProvider));
        });

        if (!isProductionEnvironment)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        return services;
    }

    /// <summary>
    /// Adds a MicrosoftSQL database to the service collection for the specified DbContext type and configuration key.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext.</typeparam>
    /// <param name="services">The IServiceCollection instance to add the MicrosoftSQL database to.</param>
    /// <param name="configKey">The configuration key for the MicrosoftSQL database settings.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
    public static IServiceCollection AddMicrosoftSql<TDbContext>(this IServiceCollection services, string configKey) where TDbContext : DbContext
    {
        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ArgumentNullException.ThrowIfNull(serviceProvider);

        bool isProductionEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>().IsProduction();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
        SqlSetting sqlSetting = configuration.GetRequiredSection(configKey).Get<SqlSetting>();

        services.AddDbContext<TDbContext>(contextOptions =>
        {
            contextOptions.ConfigureCustomWarnings();
            contextOptions.ConfigureCustomLogs(isProductionEnvironment);
            
            contextOptions.UseSqlServer(sqlSetting.ConnectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(TDbContext)).FullName);
                sqlOptions.CommandTimeout((int)TimeSpan.FromSeconds(60).TotalSeconds);

                if (sqlSetting.IsSplitQuery)
                {
                    sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                }
            });

            contextOptions.UseLazyLoadingProxies(false);
            contextOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);

            contextOptions.AddInterceptors(new SlowQueryInterceptor(serviceProvider));
        });

        if (!isProductionEnvironment)
        {
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        return services;
    }
}