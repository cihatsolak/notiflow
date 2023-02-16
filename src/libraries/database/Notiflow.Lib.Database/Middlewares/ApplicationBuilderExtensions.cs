namespace Notiflow.Lib.Database.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add migration endpoint
        /// </summary>
        /// <remarks>entity framework ile veri tabanı hatası aldığınızda devreye girer.</remarks>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            IWebHostEnvironment webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsLiveEnvironment())
                return app;

            app.UseMigrationsEndPoint();

            return app;
        }
    }
}
