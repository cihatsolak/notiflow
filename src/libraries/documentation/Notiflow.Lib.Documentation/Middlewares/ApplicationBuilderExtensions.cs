namespace Notiflow.Lib.Documentation.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds swagger documentation
        /// </summary>
        /// <param name="app">type of built-in application builder interface</param>
        /// <returns>type of built-in application builder interface</returns>
        /// <seealso cref="https://swagger.io/"/>
        public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
        {
            IWebHostEnvironment webHostEnvironment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = app.ApplicationServices.GetRequiredService<ISwaggerSetting>();

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
    }
}
