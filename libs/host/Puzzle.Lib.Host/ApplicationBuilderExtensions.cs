namespace Puzzle.Lib.Host;

/// <summary>
/// Provides extension methods for configuring exception handling middleware in an application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds exception handling middleware to the pipeline to handle and log exceptions in a production environment, or displays detailed exception information for developers in a non-production environment.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to add the middleware to.</param>
    /// <param name="redirectRoute">The route to redirect to in the event of an exception in a production environment.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseUIExceptionHandler(this IApplicationBuilder app, string redirectRoute = null)
    {
        var hostEnvironment = app.ApplicationServices.GetRequiredService<IHostEnvironment>();
        if (hostEnvironment.IsProduction())
        {
            app.UseExceptionHandler(redirectRoute);
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        return app;
    }

    /// <summary>
    /// Registers an API exception handler middleware that catches unhandled exceptions during API requests and returns a JSON error response.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance used to configure the middleware pipeline.</param>
    /// <returns>The IApplicationBuilder instance after the middleware has been registered.</returns>
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(applicationBuilder =>
        {
            applicationBuilder.Run(async httpContext =>
            {
                IExceptionHandlerFeature exceptionHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature is null)
                    return;

                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                httpContext.Response.StatusCode = exceptionHandlerFeature.Error switch
                {
                    OperationCanceledException => StatusCodes.Status503ServiceUnavailable,
                    _ => StatusCodes.Status500InternalServerError
                };

                Log.Error(exceptionHandlerFeature.Error, "-- HTTP {@httpStatusCode} -- {@message}", httpContext.Response.StatusCode, exceptionHandlerFeature.Error?.Message);

                ErrorHandlerResponse errorHandlerResponse = new()
                {
                    Code = 9000,
                    Message = "We are unable to process your transaction at this time."
                };

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(errorHandlerResponse));
            });
        });

        return app;
    }
}