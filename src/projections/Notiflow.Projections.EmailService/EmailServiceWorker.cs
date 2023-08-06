namespace Notiflow.Projections.EmailService;

public sealed class EmailServiceWorker : BackgroundService
{
    private readonly ILogger<EmailServiceWorker> _logger;

    public EmailServiceWorker(ILogger<EmailServiceWorker> logger)
    {
        _logger = logger;
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting worker service for emails...");

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker service for emails is running...");

        return Task.CompletedTask;
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker service stopped for emails...");

        return base.StopAsync(cancellationToken);
    }
}
