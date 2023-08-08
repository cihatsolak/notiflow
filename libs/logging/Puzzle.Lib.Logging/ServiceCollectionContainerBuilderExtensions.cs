namespace Puzzle.Lib.Logging;

/// <summary>
/// Provides extension methods for configuring logging options for various operations.
/// </summary>
public static class ServiceCollectionContainerBuilderExtensions
{
    /// <summary>
    /// Adds custom HTTP logging options to the service collection.
    /// </summary>
    /// <param name="services">The service collection instance.</param>
    /// <returns>The updated service collection instance.</returns>
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
