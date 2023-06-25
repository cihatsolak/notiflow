namespace Puzzle.Lib.Performance.Middlewares;

/// <summary>
/// Provides extension methods for the <see cref="IApplicationBuilder"/> interface related to response compression middleware.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds response compression middleware to the pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseResponseCompress(this IApplicationBuilder app)
    {
        return app.UseResponseCompression();
    }
}
