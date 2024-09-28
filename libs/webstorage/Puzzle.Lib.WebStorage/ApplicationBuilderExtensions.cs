namespace Puzzle.Lib.WebStorage;

/// <summary>
/// Provides extension methods for configuring the cookie policy for an IApplicationBuilder instance.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds middleware to the IApplicationBuilder instance that enforces a strict secure cookie policy.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance to add the middleware to.</param>
    /// <returns>The IApplicationBuilder instance with the middleware added.</returns>
    public static IApplicationBuilder UseAlwaysSecureCookiePolicy(this IApplicationBuilder app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            HttpOnly = HttpOnlyPolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
        });

        return app;
    }

    /// <summary>
    /// Adds the UseSession middleware with custom session options to the specified IApplicationBuilder.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance.</param>
    /// <returns>The IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseConfiguredSession(this IApplicationBuilder app)
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
