namespace Notiflow.Backoffice.Application.Features.Notifications;

public sealed record CustomerAddedNotification(int CustomerId) : INotification;

public sealed class CustomerAddedNotificationHandler : INotificationHandler<CustomerAddedNotification>
{
    private readonly ILogger<CustomerAddedNotificationHandler> _logger;

    public CustomerAddedNotificationHandler(ILogger<CustomerAddedNotificationHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerAddedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("A new customer has been added. Customer ID: {customerId}", notification.CustomerId);

        return Task.CompletedTask;
    }
}
