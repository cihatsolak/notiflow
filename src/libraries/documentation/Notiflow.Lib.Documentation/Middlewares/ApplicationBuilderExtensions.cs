namespace Notiflow.Lib.Documentation.Middlewares
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds swagger documentation
        /// </summary>
        /// <param name="app">type of web application</param>
        /// <returns>type of web application</returns>
        /// <seealso cref="https://swagger.io/"/>
        public static WebApplication UseSwaggerDoc(this WebApplication app)
        {
            IWebHostEnvironment webHostEnvironment = app.Services.GetRequiredService<IWebHostEnvironment>();
            if (webHostEnvironment.IsProduction())
                return app;

            ISwaggerSetting swaggerSetting = app.Services.GetRequiredService<ISwaggerSetting>();

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
