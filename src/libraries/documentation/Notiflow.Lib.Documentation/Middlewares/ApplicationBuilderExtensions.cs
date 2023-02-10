namespace Notiflow.Lib.Documentation.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds swagger documentation
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        /// <see cref="https://swagger.io/"/>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = serviceProvider.GetRequiredService<ISwaggerSetting>();

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUIOptions =>
            {
                swaggerUIOptions.SwaggerEndpoint(Configurations.EndpointUrl, swaggerSetting.DefinitionName);
                swaggerUIOptions.RoutePrefix = string.Empty;

                if (swaggerSetting.IsClosedSchema)
                {
                    swaggerUIOptions.DefaultModelsExpandDepth(-1);
                }
            });

            return app;
        }

        /// <summary>
        /// Add redocly documentation
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        /// <see cref="https://redocly.com/"/>
        public static IApplicationBuilder UseRedoclyDoc(this IApplicationBuilder app)
        {
            IServiceProvider serviceProvider = app.ApplicationServices;

            IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = serviceProvider.GetRequiredService<ISwaggerSetting>();

            app.UseReDoc(options =>
            {
                options.DocumentTitle = swaggerSetting.Title;
                options.SpecUrl = Configurations.EndpointUrl;
            });

            return app;
        }
    }
}
