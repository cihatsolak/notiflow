namespace Puzzle.Lib.Http.Infrastructure.Handlers;

public sealed class CorrelationIdHandler(
    IHttpContextAccessor httpContextAccessor, 
    ILogger<CorrelationIdHandler> logger) : DelegatingHandler
{
    private const string X_CORRELATION_ID = "x-correlation-id";

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        string correlationId = httpContextAccessor.HttpContext.Request.Headers[X_CORRELATION_ID];
        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            request.Headers.Add(X_CORRELATION_ID, correlationId);
        }
        else
        {
            logger.LogInformation("Relationship id -- x-correlation-id -- not found when sending request to {@RequestUri}.", request.RequestUri);
        }

        return base.SendAsync(request, cancellationToken);
    }
}
