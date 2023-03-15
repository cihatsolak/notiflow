namespace Puzzle.Lib.Hangfire.Middlewares
{
    /// <summary>
    /// Extension method for the IApplicationBuilder interface that adds the ability to use Hangfire to manage background jobs in a web application.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configures Hangfire with the specified settings and options.
        /// </summary>
        /// <param name="app">The IApplicationBuilder instance to configure.</param>
        /// <returns>The configured IApplicationBuilder instance.</returns>
        public static IApplicationBuilder UseHangfire(this IApplicationBuilder app)
        {
            HangfireSetting hangfireSetting = app.ApplicationServices.GetRequiredService<IOptions<HangfireSetting>>().Value;

            app.UseHangfireDashboard(hangfireSetting.DashboardPath, new DashboardOptions
            {
                DashboardTitle = hangfireSetting.DashboardTitle,
                AppPath = hangfireSetting.BackButtonPath,
                Authorization = new[] { new HangfireCustomBasicAuthenticationFilter
                {
                    User = hangfireSetting.Username,
                    Pass = hangfireSetting.Password
                } },
                IgnoreAntiforgeryToken = true
            });

            GlobalJobFilters.Filters.Add(new AutomaticRetryAttribute { Attempts = hangfireSetting.GlobalAutomaticRetryAttempts });
            GlobalConfiguration.Configuration.UseActivator(new HangfireActivator(app.ApplicationServices));

            return app;
        }
    }
}
