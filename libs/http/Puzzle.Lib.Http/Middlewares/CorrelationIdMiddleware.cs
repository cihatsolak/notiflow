namespace Puzzle.Lib.Http.Middlewares;

public sealed class CorrelationIdMiddleware
{
    private const string X_CORRELATION_ID = "x-correlation-id";

    private readonly RequestDelegate _next;
    private readonly IHttpContextAccessor _httpContextAccesor;

    public CorrelationIdMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccesor)
    {
        _next = next;
        _httpContextAccesor = httpContextAccesor;
    }

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

        _httpContextAccesor.HttpContext.Request.Headers.TryAdd(X_CORRELATION_ID, correlationId);

        await _next(httpContext);

        if (!httpContext.Response.Headers.ContainsKey(X_CORRELATION_ID))
        {
            httpContext.Response.Headers.Append(X_CORRELATION_ID, correlationId);
        }
    }
}
