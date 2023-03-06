namespace Puzzle.Lib.File.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add ftp setting
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <returns>type of built-in service collection interface</returns>
        /// <exception cref="ArgumentNullException">thrown when the service provider cannot be built</exception>
        public static void AddFtpSetting(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<FtpSetting>(configuration.GetRequiredSection(nameof(FtpSetting)));
            services.TryAddSingleton<IFtpSetting>(provider => provider.GetRequiredService<IOptions<FtpSetting>>().Value);
        }
    }
}
