namespace Puzzle.Lib.Cookie.Middlewares;

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
    public static IApplicationBuilder UseSecureCookiePolicy(this IApplicationBuilder app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            Secure = CookieSecurePolicy.Always,
            HttpOnly = HttpOnlyPolicy.Always,
            MinimumSameSitePolicy = SameSiteMode.Strict
        });

        return app;
    }
}
