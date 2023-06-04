namespace Notiflow.Projections.NotificationService;

public sealed class NotificationServiceWorker : BackgroundService
{
    private readonly ILogger<NotificationServiceWorker> _logger;

    public NotificationServiceWorker(ILogger<NotificationServiceWorker> logger)
    {
        _logger = logger;
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting worker service for notifications...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker service for notifications is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker service stopped for notifications...");

        return base.StopAsync(cancellationToken);
    }
}