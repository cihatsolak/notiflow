namespace Notiflow.Projections.EmailService;

public sealed class EmailServiceWorker(ILogger<EmailServiceWorker> logger) : BackgroundService
{
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting worker service for emails...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Worker service for emails is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Worker service stopped for emails...");

        return base.StopAsync(cancellationToken);
    }
}
