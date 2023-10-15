namespace Notiflow.Projections.NotificationService.Consumers;

public sealed class NotificationDeliveredEventConsumer : IConsumer<NotificationDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<NotificationDeliveredEventConsumer> _logger;

    public NotificationDeliveredEventConsumer(
        IDbConnection connection, 
        ILogger<NotificationDeliveredEventConsumer> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationDeliveredEvent> context)
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
                        is_sent = true,
                        error_message = (string)null,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The sent notification has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The sent notification could not be saved to the database.");
            throw;
        }
    }
}
