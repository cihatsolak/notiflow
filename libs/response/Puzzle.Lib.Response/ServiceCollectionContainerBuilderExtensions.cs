namespace Puzzle.Lib.Response;

/// <summary>
/// Extension methods for configuring and adding response compression services to the IServiceCollection.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds response compression services to the IServiceCollection with default configurations for Gzip and Brotli compression providers.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the response compression services to.</param>
    /// <returns>The modified IServiceCollection.</returns>
    public static IServiceCollection AddCompressResponse(this IServiceCollection services)
    {
        services.Configure<GzipCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Optimal;
        });

        services.Configure<BrotliCompressionProviderOptions>(options =>
        {
            options.Level = CompressionLevel.Fastest;
        });

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<BrotliCompressionProvider>();
            options.Providers.Add<GzipCompressionProvider>();
        });

        return services;
    }
}