namespace Notiflow.Backoffice.Application.Pipelines;

public sealed class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    private readonly Stopwatch _stopwatch;
    private readonly ILogger<TRequest> _logger;

    public PerformanceBehaviour(ILogger<TRequest> logger)
    {
        _stopwatch = new Stopwatch();
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _stopwatch.Start();
        TResponse response = await next();
        _stopwatch.Stop();

        var elapsedSeconds = _stopwatch.Elapsed.TotalSeconds;
        if (elapsedSeconds >= 40)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long Running Request: {Name} ({elapsedSeconds} seconds) {@Request}", requestName, elapsedSeconds, request);
        }

        return response;
    }
}
