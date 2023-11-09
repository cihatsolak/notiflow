namespace Puzzle.Lib.HealthCheck;

/// <summary>
/// Provides extension methods to configure the health check middleware and UI for an application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the Health Check middleware to the application pipeline, using the specified response path and health check options. Path: /health
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The updated <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHealthChecksConfiguration(this IApplicationBuilder app)
    {
        app.UseHealthChecks("/health", new HealthCheckOptions
        {
            Predicate = _ => true,
            AllowCachingResponses = false,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
        });

        return app;
    }

    /// <summary>
    /// Adds the Health Check UI middleware to the application pipeline, using the specified UI path and custom CSS file (if applicable).
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The updated <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHealthUIConfiguration(this IApplicationBuilder app, string customCssPath = null)
    {
        app.UseHealthChecksUI(setup =>
        {
            setup.UIPath = "/health-ui";

            if (!string.IsNullOrWhiteSpace(customCssPath))
            {
                setup.AddCustomStylesheet(Path.Combine(Directory.GetCurrentDirectory(), customCssPath));
            }
        });

        return app;
    }

    /// <summary>
    /// Adds both the Health Check and Health Check UI middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The updated <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHealthAndUIConfiguration(this IApplicationBuilder app)
    {
        return app.UseHealthChecksConfiguration().UseHealthUIConfiguration();
    }
}
