namespace Notiflow.Lib.Database.Middlewares
{
    /// <summary>
    /// Extension methods to add database/entity framework capabilities to an application pipeline.
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use middleware for migration endpoint
        /// </summary>
        /// <remarks>activates when there is a database error with the entity framework.</remarks>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsLiveEnvironment())
                return app;

            app.UseMigrationsEndPoint();

            return app;
        }
    }
}
