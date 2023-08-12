using Puzzle.Lib.Http.Middlewares;

namespace Puzzle.Lib.Http;

public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware for logging correlation IDs.
    /// </summary>
    /// <param name="app">The application builder instance.</param>
    /// <returns>The updated application builder instance.</returns>
    public static IApplicationBuilder UseCorrelationIdLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<CorrelationIdMiddleware>();
    }
}