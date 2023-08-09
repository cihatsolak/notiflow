namespace Puzzle.Lib.Session;

/// <summary>
/// Provides extension methods to configure and use custom session options in an application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds the UseSession middleware with custom session options to the specified IApplicationBuilder.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    /// <returns>The IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseCustomSession(IApplicationBuilder app)
    {
        app.UseSession(new SessionOptions
        {
            IdleTimeout = TimeSpan.FromMinutes(30),
            Cookie = new CookieBuilder
            {
                HttpOnly = true,
                IsEssential = true
            }
        });

        return app;
    }
}
