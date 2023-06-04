﻿namespace Notiflow.Projections.EmailService;

public sealed class EmailServiceWorker : BackgroundService
{
    private readonly ILogger<EmailServiceWorker> _logger;

    public EmailServiceWorker(ILogger<EmailServiceWorker> logger)
    {
        _logger = logger;
    }
    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting email service for notifications...");

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
