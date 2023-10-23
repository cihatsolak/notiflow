namespace Puzzle.Lib.Hangfire;

/// <summary>
/// Extension method for the IApplicationBuilder interface that adds the ability to use Hangfire to manage background jobs in a web application.
/// </summary>
public static class ApplicationBuilderExtensions
{
    private const string HANGFIRE_MAIN_PATH = "/hangfire";

    /// <summary>
    /// Configures Hangfire with the specified settings and options.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance to configure.</param>
    /// <returns>The configured IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
    {
        HangfireSetting hangfireSetting = app.ApplicationServices.GetRequiredService<IOptions<HangfireSetting>>().Value;

        app.UseHangfireDashboard(HANGFIRE_MAIN_PATH, new DashboardOptions
        {
            DashboardTitle = Assembly.GetCallingAssembly().GetName().Name,
            AppPath = HANGFIRE_MAIN_PATH,
            Authorization = new[] { new HangfireCustomBasicAuthenticationFilter
            {
                User = hangfireSetting.Username,
                Pass = hangfireSetting.Password
            } },
            IgnoreAntiforgeryToken = true
        });

        return app;
    }
}
