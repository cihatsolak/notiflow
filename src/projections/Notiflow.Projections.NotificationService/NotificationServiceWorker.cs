namespace Notiflow.Projections.NotificationService;

public sealed class NotificationServiceWorker(ILogger<NotificationServiceWorker> logger) : BackgroundService
{
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting worker service for notifications...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker service for notifications is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker service stopped for notifications...");

        return base.StopAsync(cancellationToken);
    }
}