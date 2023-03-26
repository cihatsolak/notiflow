namespace Puzzle.Lib.SeriLog.Middlewares
{
    public sealed class RequstDetectionMiddleware
    {
        private readonly RequestDelegate _next;

        public RequstDetectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext, IDetectionService detectionService)
        {
            using (LogContext.PushProperty(LogPushProperties.Device, detectionService?.Device?.Type.ToString()))
            {
                using (LogContext.PushProperty(LogPushProperties.OperatingSystem, detectionService?.Platform?.Name.ToString()))
                {
                    using (LogContext.PushProperty(LogPushProperties.Browser, detectionService?.Browser?.Name.ToString()))
                    {
                        await _next(httpContext);
                    }
                }
            }
        }
    }
}
