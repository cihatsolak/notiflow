namespace Puzzle.Lib.Security.Middlewares;

public sealed class SecurityHeadersMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Xss-Protection", "1; mode=block");
        context.Response.Headers.Append("Referrer-Policy", "no-referrer");
        context.Response.Headers.Append("X-Permitted-Cross-Domain-Policies", "none");
        context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");

        await next.Invoke(context);

        if (!context.Response.HasStarted)
        {
            context.Response.Headers.Remove("Server");
        }
    }
}