namespace Puzzle.Lib.Http.Infrastructure.Handlers;

public sealed class CorrelationIdHandler : DelegatingHandler
{
    private const string X_CORRELATION_ID = "x-correlation-id";

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CorrelationIdHandler> _logger;

    public CorrelationIdHandler(IHttpContextAccessor httpContextAccessor, ILogger<CorrelationIdHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string correlationId = _httpContextAccessor.HttpContext.Request.Headers[X_CORRELATION_ID];
        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            request.Headers.Add(X_CORRELATION_ID, correlationId);
        }
        else
        {
            _logger.LogInformation("Relationship id -- x-correlation-id -- not found when sending request to {@RequestUri}.", request.RequestUri);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
