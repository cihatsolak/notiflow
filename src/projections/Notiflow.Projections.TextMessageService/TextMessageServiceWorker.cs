namespace Notiflow.Projections.TextMessageService;

public sealed class TextMessageServiceWorker : BackgroundService
{
    private readonly ILogger<TextMessageServiceWorker> _logger;

    public TextMessageServiceWorker(ILogger<TextMessageServiceWorker> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting worker service for text messages...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker service for text messages is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker service stopped for text messages...");

        return base.StopAsync(cancellationToken);
    }
}