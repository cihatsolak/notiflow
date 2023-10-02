namespace Puzzle.Lib.Database;

/// <summary>
/// Provides extension methods for configuring database migrations during application startup.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the application to use database migrations, but only in non-live environments.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> after the configuration has been applied.</returns>
    public static IApplicationBuilder UseMigrations(this IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsProduction())
            return app;

        return app.UseMigrationsEndPoint();
    }
}
