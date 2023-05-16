namespace Puzzle.Lib.Logging.Middlewares
{
    public sealed class HttpRequestPropertyMiddleware
    {
        private readonly RequestDelegate _next;

        public HttpRequestPropertyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (LogContext.PushProperty(LogPushProperties.TraceIdentifier, httpContext?.TraceIdentifier))
            {
                using (LogContext.PushProperty(LogPushProperties.RequestMethod, httpContext?.Request.Method))
                {
                    await _next(httpContext);
                }
            }
        }
    }
}
