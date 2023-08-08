namespace Puzzle.Lib.Http.Handlers;

public sealed class CorrelationIdHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<CorrelationIdHandler> _logger;

    public CorrelationIdHandler(IHttpContextAccessor httpContextAccessor, ILogger<CorrelationIdHandler> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string correlationId = _httpContextAccessor.HttpContext.Request.Headers["x-correlation-id"];
        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            request.Headers.Add("x-correlation-id", correlationId);
        }
        else
        {
            _logger.LogInformation("Relationship id -- x-correlation-id -- not found when sending request to {@RequestUri}.", request.RequestUri);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
