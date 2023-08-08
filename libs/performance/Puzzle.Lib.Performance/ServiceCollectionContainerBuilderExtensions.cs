namespace Puzzle.Lib.Performance;

/// <summary>
/// Extension methods for adding Gzip compression to HTTP response for fastest compression.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds Gzip compression to HTTP response for fastest compression.
    /// </summary>
    /// <param name="services">The collection of services to add the compression to.</param>
    /// <returns>The collection of services with added compression.</returns>
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
    /// Adds Gzip compression to HTTP response with a specified compression level.
    /// </summary>
    /// <param name="services">The collection of services to add the compression to.</param>
    /// <param name="compressionLevel">The compression level to use.</param>
    /// <returns>The collection of services with added compression.</returns>
    public static IServiceCollection AddGzipResponseCompress(this IServiceCollection services, CompressionLevel compressionLevel)
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
