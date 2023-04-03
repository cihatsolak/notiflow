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

        /// <summary>
        /// An IServiceCollection extension that enables the HTTP Strict Transport Security (HSTS) feature.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>

        public static IServiceCollection AddStrictTransportSecurity(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (!webHostEnvironment.IsProduction())
                return services;

            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.MaxValue;
            });

            return services;
        }

        /// <summary>
        /// An IServiceCollection extension that adds a data protection service for the application and registers an implementation of IProtectorService.
        /// </summary>
        /// <param name="services">The IServiceCollection instance.</param>
        /// <returns>The IServiceCollection instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the <see cref="IServiceProvider"/> instance obtained from the specified <see cref="IServiceCollection"/> is null.</exception>
        public static IServiceCollection AddProtectorService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            string applicationName = $"{webHostEnvironment.ApplicationName}.{webHostEnvironment.EnvironmentName}.dataprotection.key".ToLowerInvariant();

            services.AddDataProtection()
                .SetApplicationName(applicationName);

            services.TryAddSingleton<IProtectorService, ProtectorManager>();

            return services;
        }
    }
}