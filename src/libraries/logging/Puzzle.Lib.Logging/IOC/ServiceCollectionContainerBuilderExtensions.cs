namespace Puzzle.Lib.Logging.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        public static IServiceCollection AddRequestDetection(this IServiceCollection services)
        {
            services.AddDetection();
            return services;
        }

        public static IServiceCollection AddProtocolService(this IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ArgumentNullException.ThrowIfNull(serviceProvider);

            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
            services.Configure<HostingSetting>(configuration.GetRequiredSection(nameof(HostingSetting)));

            services.TryAddSingleton<IProtocolService, ProtocolManager>();

            return services;
        }

        public static IServiceCollection AddCustomHttpLogging(this IServiceCollection services)
        {
            services.AddHttpLogging(httpLoggingOptions =>
            {
                httpLoggingOptions.LoggingFields = HttpLoggingFields.All;
                httpLoggingOptions.RequestHeaders.Add("sec-ch-ua");
                httpLoggingOptions.MediaTypeOptions.AddText("application/javascript");
                httpLoggingOptions.RequestBodyLogLimit = 4096;
                httpLoggingOptions.ResponseBodyLogLimit = 4096;
            });

            return services;
        }
    }
}
