namespace Notiflow.Projections.TextMessageService;

public sealed class TextMessageServiceWorker(ILogger<TextMessageServiceWorker> logger) : BackgroundService
{
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting worker service for text messages...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker service for text messages is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker service stopped for text messages...");

        return base.StopAsync(cancellationToken);
    }
}