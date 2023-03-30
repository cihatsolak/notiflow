namespace Puzzle.Lib.File.IOC
{
    /// <summary>
    /// Extension methods for registering FTP settings to the service collection.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds FTP settings to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the FTP settings to.</param>
        /// <exception cref="ArgumentNullException">Thrown when the service provider is null.</exception>
        public static void AddFtpSetting(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<FtpSetting>(configuration.GetRequiredSection(nameof(FtpSetting)));
        }
    }
}
