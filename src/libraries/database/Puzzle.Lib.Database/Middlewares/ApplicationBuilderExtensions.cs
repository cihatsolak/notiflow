namespace Puzzle.Lib.Database.Middlewares

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            if (app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsLiveEnvironment())
                return app;

            app.UseMigrationsEndPoint();

            return app;
        }
    }
}
