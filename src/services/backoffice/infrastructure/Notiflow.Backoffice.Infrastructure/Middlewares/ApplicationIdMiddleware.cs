namespace Notiflow.Backoffice.Infrastructure.Middlewares;

public sealed class ApplicationIdMiddleware
{
    private readonly RequestDelegate _next;

    public ApplicationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        //if (!httpContext.Request.Headers.ContainsKey("x-application-id"))
        //{
        //    httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //    httpContext.Response.ContentType = MediaTypeNames.Application.Json;
        //    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(ResponseModel<Unit>.Fail(1, "authorization error")));

        //    return;
        //}

        await _next(httpContext);
    }
}
