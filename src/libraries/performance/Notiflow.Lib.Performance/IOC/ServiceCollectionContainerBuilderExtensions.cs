using Microsoft.AspNetCore.Builder;

namespace Notiflow.Lib.Performance.IOC
{
    public static class ServiceCollectionContainerBuilderExtensions
    {
        /// <summary>
        /// Add gzip response compression
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-6.0"/>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddGzipResponseFastestCompress(this IServiceCollection services)
        {
            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }

        /// <summary>
        /// Add gzip response compression
        /// </summary>
        /// <param name="services">type of built-in service collection interface</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-6.0"/>
        /// <returns>type of built-in service collection interface</returns>
        public static IServiceCollection AddGzipResponseCompress(this IServiceCollection services, CompressionLevel compressionLevel = CompressionLevel.Fastest)
        {
            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = compressionLevel;
            });

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });

            return services;
        }
    }
}
