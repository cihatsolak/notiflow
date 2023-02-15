namespace Notiflow.Lib.Database.Infrastructure.Extensions
{
    /// <summary>
    /// Migration manager extensions with microsoft sql server
    /// </summary>
    public static class MigrationManagerExtensions
    {
        /// <summary>
        /// Migrate database
        /// </summary>
        /// <typeparam name="TDbContext">type of db context</typeparam>
        /// <param name="services">type of web application class</param>
        /// <returns>type of web application class</returns>
        public static async Task<IServiceCollection> MigrateDatabaseAsync<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            //if (!services.Environment.EnvironmentName.Equals("Todo"))
            //    return services;

            //await using AsyncServiceScope asyncServiceScope = services.Services.CreateAsyncScope();
            //IServiceProvider serviceProvider = asyncServiceScope.ServiceProvider;

            
            ILogger logger = Log.ForContext(typeof(MigrationManagerExtensions));

            try
            {
                TDbContext context = serviceProvider.GetRequiredService<TDbContext>();
                if (!context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
                {
                    await context.Database.MigrateAsync();
                    logger.Information("Migration completed successfully.");
                }

                return services;
            }
            catch (Exception exception)
            {
                logger.Error(exception, "The migration process ended with an error.");
                return services;
            }
        }
    }
}
