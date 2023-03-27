namespace Puzzle.Lib.Logging.Middlewares
{
    public sealed class IpAddressMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IProtocolService _protocolService;

        public IpAddressMiddleware(RequestDelegate next, IProtocolService protocolService)
        {
            _next = next;
            _protocolService = protocolService;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (LogContext.PushProperty(LogPushProperties.IpAddress, _protocolService.IpAddress))
            {
                await _next(httpContext);
            }
        }
    }
}
