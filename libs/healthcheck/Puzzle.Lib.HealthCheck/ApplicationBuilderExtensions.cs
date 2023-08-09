namespace Puzzle.Lib.HealthCheck;

/// <summary>
/// Provides extension methods to configure the health check middleware and UI for an application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the Health Check middleware to the application pipeline, using the specified response path and health check options.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The updated <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHealth(this IApplicationBuilder app)
    {
        HealthUISetting healthUISetting = app.ApplicationServices.GetRequiredService<IOptions<HealthUISetting>>().Value;

        app.UseHealthChecks(healthUISetting.ResponsePath, new HealthCheckOptions
        {
            Predicate = _ => true,
            AllowCachingResponses = false,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
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
    public static IApplicationBuilder UseHealthUI(this IApplicationBuilder app)
    {
        HealthUISetting healthUISetting = app.ApplicationServices.GetRequiredService<IOptions<HealthUISetting>>().Value;

        app.UseHealthChecksUI(setup =>
        {
            setup.UIPath = healthUISetting.UIPath;

            if (!string.IsNullOrWhiteSpace(healthUISetting.CustomCssPath))
            {
                setup.AddCustomStylesheet(Path.Combine(Directory.GetCurrentDirectory(), healthUISetting.CustomCssPath));
            }
        });

        return app;
    }

    /// <summary>
    /// Adds both the Health Check and Health Check UI middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The updated <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHealthAndHealthUI(this IApplicationBuilder app)
    {
        return app.UseHealth().UseHealthUI();
    }
}
