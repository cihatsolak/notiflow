namespace Notiflow.Projections.NotificationService.Consumers;

internal sealed class NotificationNotDeliveredEventConsumer(
    IDbConnection connection,
    ILogger<NotificationNotDeliveredEventConsumer> logger) : IConsumer<NotificationNotDeliveredEvent>
{
    public async Task Consume(ConsumeContext<NotificationNotDeliveredEvent> context)
    {
        try
        {
            await connection
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

            logger.LogInformation("The notification that could not be sent has been saved in the database.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "The not sent notification could not be saved to the database.");
            throw;
        }
    }
}
