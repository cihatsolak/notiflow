namespace Puzzle.Lib.Http.Middlewares;

public sealed class CorrelationIdMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccesor)
{
    private const string X_CORRELATION_ID = "x-correlation-id";

    public async Task InvokeAsync(HttpContext httpContext)
    {
        string correlationId;

        bool isCorrelationIdExists = httpContext.Request.Headers.TryGetValue(X_CORRELATION_ID, out var values);
        if (isCorrelationIdExists)
        {
            correlationId = values[0];
        }
        else
        {
            correlationId = Guid.NewGuid().ToString();
        }

        httpContextAccesor.HttpContext.Request.Headers.TryAdd(X_CORRELATION_ID, correlationId);

        await next(httpContext);

        if (!httpContext.Response.Headers.ContainsKey(X_CORRELATION_ID))
        {
            httpContext.Response.Headers.Append(X_CORRELATION_ID, correlationId);
        }
    }
}
