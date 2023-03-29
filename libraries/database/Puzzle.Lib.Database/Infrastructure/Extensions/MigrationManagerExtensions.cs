namespace Puzzle.Lib.Database.Infrastructure.Extensions
{
    /// <summary>
    /// Migration manager extensions
    /// <see cref="DbContext"/> 
    /// <see cref="IServiceCollection"/>
    /// <see cref="IWebHostEnvironment"/>
    /// </summary>
    public static class MigrationManagerExtensions
    {
        /// <summary>
        /// Migrate database
        /// </summary>
        /// <typeparam name="TDbContext">type of db context</typeparam>
        /// <param name="services">type of built-in service collection interface</param>
        public static async Task MigrateDatabaseAsync<TDbContext>(this IServiceCollection services) where TDbContext : DbContext
        {
            await using AsyncServiceScope asyncServiceScope = services.BuildServiceProvider().CreateAsyncScope();
            IServiceProvider serviceProvider = asyncServiceScope.ServiceProvider;

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
                return;

            ILogger logger = Log.ForContext(typeof(MigrationManagerExtensions));

            try
            {
                TDbContext context = serviceProvider.GetRequiredService<TDbContext>();
                if (!context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
                {
                    await context.Database.MigrateAsync();
                    logger.Information("Migration completed successfully.");
                }
            }
            catch (Exception exception)
            {
                logger.Error(exception, "The migration process ended with an error.");
            }
        }
    }
}
