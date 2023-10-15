namespace Notiflow.Projections.NotificationService.Consumers;

internal class NotificationNotDeliveredEventConsumer : IConsumer<NotificationNotDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<NotificationNotDeliveredEventConsumer> _logger;

    public NotificationNotDeliveredEventConsumer(
        IDbConnection connection, 
        ILogger<NotificationNotDeliveredEventConsumer> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationNotDeliveredEvent> context)
    {
        try
        {
            await _connection
                    .ExecuteAsync("insert into notificationhistory (title, message, image_url, sender_identity, is_sent, error_message, sent_date, customer_id) values (@title, @message, @image_url, @sender_identity, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        title = context.Message.Title,
                        message = context.Message.Message,
                        image_url = context.Message.ImageUrl,
                        sender_identity = context.Message.SenderIdentity,
                        is_sent = false,
                        error_message = context.Message.ErrorMessage,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The notification that could not be sent has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The not sent notification could not be saved to the database.");
            throw;
        }
    }
}
