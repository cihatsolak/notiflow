namespace Puzzle.Lib.Documentation;

/// <summary>
/// Provides extension methods for configuring Swagger and ReDoc documentation in the application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Represents the URL path for the Swagger JSON file in the application.
    /// </summary>
    private const string SWAGGER_JSON_URL = "/swagger/v1/swagger.json";

    /// <summary>
    /// Adds Swagger middleware and Swagger UI to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(swaggerUIOptions =>
        {
            swaggerUIOptions.SwaggerEndpoint(SWAGGER_JSON_URL, Assembly.GetEntryAssembly().GetName().Name);
            swaggerUIOptions.DefaultModelsExpandDepth(-1);
            swaggerUIOptions.RoutePrefix = string.Empty;
        });

        return app;
    }

    /// <summary>
    /// Adds ReDoc middleware to the application pipeline for displaying the OpenAPI specification.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseRedoclyDoc(this IApplicationBuilder app)
    {
        SwaggerSetting swaggerSetting = app.ApplicationServices.GetRequiredService<IOptions<SwaggerSetting>>().Value;

        app.UseReDoc(options =>
        {
            options.DocumentTitle = swaggerSetting.Title;
            options.SpecUrl = SWAGGER_JSON_URL;
        });

        return app;
    }

    /// <summary>
    /// Adds both Swagger middleware and ReDoc middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseSwaggerRedocly(this IApplicationBuilder app)
    {
        app.UseSwaggerDoc().UseRedoclyDoc();

        return app.UseSwaggerBasicAuth();
    }

    /// <summary>
    /// Adds Swagger basic authentication middleware to the ASP.NET Core application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseSwaggerBasicAuth(this IApplicationBuilder app)
    {
        return app.UseMiddleware<SwaggerBasicAuthenticationMiddleware>();
    }
}
