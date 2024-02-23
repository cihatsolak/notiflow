namespace Puzzle.Lib.Host;

/// <summary>
/// Provides extension methods for configuring exception handling middleware in an application pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    private const string DEFAULT_ERROR_MESSAGE = "We are unable to process your transaction at this time.";

    /// <summary>
    /// Adds exception handling middleware to the pipeline to handle and log exceptions in a production environment, or displays detailed exception information for developers in a non-production environment.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> instance to add the middleware to.</param>
    /// <param name="errorHandlingPath">The route to redirect to in the event of an exception in a production environment.</param>
    /// <returns>The <see cref="IApplicationBuilder"/> instance.</returns>
    public static IApplicationBuilder UseUIExceptionHandler(this WebApplication webApplication, string errorHandlingPath = null)
    {
        if (webApplication.Environment.IsProduction())
        {
            webApplication.UseExceptionHandler(errorHandlingPath);
        }
        else
        {
            webApplication.UseDeveloperExceptionPage();
        }

        return webApplication;
    }

    /// <summary>
    /// Registers an API exception handler middleware that catches unhandled exceptions during API requests and returns a JSON error response.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance used to configure the middleware pipeline.</param>
    /// <returns>The IApplicationBuilder instance after the middleware has been registered.</returns>
    public static IApplicationBuilder UseApiExceptionHandler(this WebApplication webApplication)
    {
        webApplication.UseExceptionHandler(applicationBuilder =>
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

                webApplication.Logger.LogError(exceptionHandlerFeature.Error, "{message}", exceptionHandlerFeature.Error?.Message);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Message = DEFAULT_ERROR_MESSAGE
                }));
            });
        });

        return webApplication;
    }

    /// <summary>
    /// Registers an API exception handler middleware that catches unhandled exceptions during API requests and returns a JSON error response.
    /// </summary>
    /// <param name="app">The IApplicationBuilder instance used to configure the middleware pipeline.</param>
    /// <param name="errorMessage"></param>
    /// <returns>The IApplicationBuilder instance after the middleware has been registered.</returns>
    public static IApplicationBuilder UseApiExceptionHandler(this WebApplication webApplication, string errorMessage)
    {
        webApplication.UseExceptionHandler(applicationBuilder =>
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

                webApplication.Logger.LogError(exceptionHandlerFeature.Error, "{message}", exceptionHandlerFeature.Error?.Message);

                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Message = errorMessage
                }));
            });
        });

        return webApplication;
    }

    public static IApplicationBuilder UseDiscoveryEndpoint(this WebApplication app)
    {
        app.MapGet("api/discovery", () =>
        {
            FileInfo fileInfo = new(Assembly.GetEntryAssembly().Location);

            DiscoveryResponse discoveryResponse =
                    new(fileInfo.LastWriteTime,
                        fileInfo.LastWriteTimeUtc,
                        Environment.MachineName,
                        Environment.OSVersion.VersionString,
                        RuntimeInformation.FrameworkDescription);

            return discoveryResponse;
        })
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Displays discovery information and details about the application.",
            Description = "Displays discovery information and details about the application."
        })
        .Produces(StatusCodes.Status200OK, typeof(DiscoveryResponse), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status401Unauthorized, typeof(UnauthorizedObjectResult), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status404NotFound, typeof(NotFoundObjectResult), MediaTypeNames.Application.Json)
        .Produces(StatusCodes.Status500InternalServerError, typeof(ProblemDetails), MediaTypeNames.Application.Json)
        .WithTags("General");

        return app;
    }
}