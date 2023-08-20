﻿namespace Puzzle.Lib.Documentation;

/// <summary>
/// Provides extension methods for configuring Swagger and ReDoc documentation in the application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds Swagger middleware and Swagger UI to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseSwaggerDoc(this IApplicationBuilder app)
    {
        IServiceProvider serviceProvider = app.ApplicationServices;
        IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (webHostEnvironment.IsProduction())
            return app;

        app.UseSwagger();
        app.UseSwaggerUI(swaggerUIOptions =>
        {
            swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", Assembly.GetEntryAssembly().GetName().Name);
            swaggerUIOptions.RoutePrefix = string.Empty;
            swaggerUIOptions.DefaultModelsExpandDepth(-1);
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
        IServiceProvider serviceProvider = app.ApplicationServices;
        IWebHostEnvironment webHostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

        if (webHostEnvironment.IsProduction())
            return app;

        SwaggerSetting swaggerSetting = serviceProvider.GetRequiredService<IOptions<SwaggerSetting>>().Value;

        app.UseReDoc(options =>
        {
            options.DocumentTitle = swaggerSetting.Title;
            options.SpecUrl = "/swagger/v1/swagger.json";
        });

        return app;
    }

    /// <summary>
    /// Adds both Swagger middleware and ReDoc middleware to the application pipeline.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseSwaggerWithRedoclyDoc(this IApplicationBuilder app)
    {
        return app.UseSwaggerDoc().UseRedoclyDoc();
    }
}