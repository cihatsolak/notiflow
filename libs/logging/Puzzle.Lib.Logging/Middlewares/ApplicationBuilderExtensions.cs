namespace Puzzle.Lib.Logging.Middlewares;

/// <summary>
/// Provides extension methods for configuring logging options for various operations.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware for logging HTTP request properties.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseHttpRequestPropertyLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpRequestPropertyMiddleware>();
    }

    /// <summary>
    /// Adds middleware for logging correlation IDs.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }

    /// <summary>
    /// Adds middleware for logging HTTP requests and responses using Serilog.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseCustomSeriLogging(this IApplicationBuilder app) 
    {
        return app.UseSerilogRequestLogging().UseHttpRequestPropertyLogging().UseCorrelationIdLogging();
    }

    /// <summary>
    /// Adds middleware for logging HTTP requests and responses.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseCustomHttpLogging(this IApplicationBuilder app)
    {
        return app.UseHttpLogging();
    }
}
