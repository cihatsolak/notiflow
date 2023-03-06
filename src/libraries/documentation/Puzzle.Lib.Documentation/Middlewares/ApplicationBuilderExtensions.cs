namespace Puzzle.Lib.Documentation.Middlewares
{
    /// <summary>
    /// Extension methods to add documentation capabilities to an application pipeline. <see cref="IApplicationBuilder"/>
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Use middleware for swagger documentation
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        /// <see cref="https://swagger.io/"/>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;

            if (serviceProvider.GetRequiredService<IWebHostEnvironment>().IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = serviceProvider.GetRequiredService<ISwaggerSetting>();

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", swaggerSetting.DefinitionName);
                swaggerUIOptions.RoutePrefix = string.Empty;

                if (swaggerSetting.IsClosedSchema)
                {
                    swaggerUIOptions.DefaultModelsExpandDepth(-1);
                }
            });

            return app;
        }

        /// <summary>
        /// Use middleware for Redocly documentation
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        /// <see cref="https://redocly.com/"/>
        public static IApplicationBuilder UseRedoclyDoc(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;

            if (serviceProvider.GetRequiredService<IWebHostEnvironment>().IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = serviceProvider.GetRequiredService<ISwaggerSetting>();

            app.UseReDoc(options =>
            {
                options.DocumentTitle = swaggerSetting.Title;
                options.SpecUrl = "/swagger/v1/swagger.json";
            });

            return app;
        }
    }
}
