﻿namespace Puzzle.Lib.Security;

/// <summary>
/// Provides extension methods for <see cref="IApplicationBuilder"/> to add request detection logging functionality.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds request detection logging functionality to the specified <see cref="IApplicationBuilder"/>.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to add the request detection logging functionality to.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseRequestDetection(this IApplicationBuilder app)
    {
        return app.UseDetection();
    }

    /// <summary>
    /// Adds HTTP security measures to the request pipeline, including HTTPS redirection, HTTP Strict Transport Security (HSTS), and setting security headers with the <see cref="SecurityHeadersMiddleware"/>.
    /// This middleware is only applied when the application is running in production environment.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseHttpSecurityPrecautions(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseHsts();
        app.UseMiddleware<SecurityHeadersMiddleware>();

        return app;
    }

    /// <summary>
    /// Adds custom CORS policy to the specified application builder using the executing assembly's full name.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The updated application builder with the custom CORS policy added.</returns>
    public static IApplicationBuilder UseCustomCors(IApplicationBuilder app)
    {
        return app.UseCors(Assembly.GetCallingAssembly().GetName().Name);
    }

    /// <summary>
    /// Configures the application to use forwarded headers for processing client information.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to configure.</param>
    /// <returns>The modified <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseForwardedHeader(IApplicationBuilder app)
    {
        return app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
    }
}