namespace Puzzle.Lib.Host.Middlewares
{
    /// <summary>
    /// Provides extension methods for configuring exception handling middleware in an application pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds exception handling middleware to the pipeline to handle and log exceptions in a production environment, or displays detailed exception information for developers in a non-production environment.
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/> instance to add the middleware to.</param>
        /// <param name="redirectRoute">The route to redirect to in the event of an exception in a production environment.</param>
        /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseExceptionPage(this IApplicationBuilder app, string redirectRoute = null)
        {
            var webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
            {
                app.UseExceptionHandler(redirectRoute);
            }
            else
            {
                app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }

}
