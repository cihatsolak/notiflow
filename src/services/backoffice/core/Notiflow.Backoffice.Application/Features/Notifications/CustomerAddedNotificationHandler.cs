namespace Notiflow.Backoffice.Application.Features.Notifications;

public sealed record CustomerAddedNotification(int CustomerId) : INotification;

public sealed class CustomerAddedNotificationHandler(ILogger<CustomerAddedNotificationHandler> logger) 
    : INotificationHandler<CustomerAddedNotification>
{
    public Task Handle(CustomerAddedNotification notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("A new customer has been added. Customer ID: {customerId}", notification.CustomerId);

        return Task.CompletedTask;
    }
}
