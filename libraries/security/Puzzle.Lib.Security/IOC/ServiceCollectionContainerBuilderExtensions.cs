namespace Puzzle.Lib.Security.IOC
{
    /// <summary>
    /// Provides extension methods for <see cref="IServiceCollection"/> to add request detection functionality.
    /// </summary>
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Adds request detection functionality to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the request detection functionality to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddRequestDetection(this IServiceCollection services)
        {
            services.AddDetection();

            return services;
        }

        /// <summary>
        /// Adds protocol service functionality to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the protocol service functionality to.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
        public static IServiceCollection AddProtocolService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<HostingSetting>(configuration.GetRequiredSection(nameof(HostingSetting)));

            services.TryAddSingleton<IProtocolService, ProtocolManager>();

            return services;
        }
    }
}