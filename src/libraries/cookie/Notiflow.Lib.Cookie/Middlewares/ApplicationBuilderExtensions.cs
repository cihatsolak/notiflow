namespace Notiflow.Lib.Cookie.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use cookie policy options
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        public static IApplicationBuilder UseCookiePolicyOptions(this IApplicationBuilder app)
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
}
