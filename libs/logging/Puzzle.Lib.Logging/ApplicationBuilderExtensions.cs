namespace Puzzle.Lib.Logging;

/// <summary>
/// Provides extension methods for configuring logging options for various operations.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware for logging HTTP requests and responses using Serilog.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseSerilogLogging(this IApplicationBuilder app)
    {
        return app.UseSerilogRequestLogging();
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
