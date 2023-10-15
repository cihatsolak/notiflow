namespace Puzzle.Lib.Http.Middlewares;

public sealed class CorrelationIdMiddleware
{
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

        bool isCorrelationIdExists = httpContext.Request.Headers.TryGetValue("x-correlation-id", out var values);
        if (isCorrelationIdExists)
        {
            correlationId = values.First();
        }
        else
        {
            correlationId = Guid.NewGuid().ToString();
        }

        _httpContextAccesor.HttpContext.Request.Headers.TryAdd("x-correlation-id", correlationId);

        await _next(httpContext);

        if (!httpContext.Response.Headers.ContainsKey("x-correlation-id"))
        {
            httpContext.Response.Headers.Add("x-correlation-id", correlationId);
        }
    }
}
