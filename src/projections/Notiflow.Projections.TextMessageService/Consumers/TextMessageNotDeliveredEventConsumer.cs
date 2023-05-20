namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageNotDeliveredEventConsumer : IConsumer<TextMessageNotDeliveredEvent>
{
    public async Task Consume(ConsumeContext<TextMessageNotDeliveredEvent> context)
    {
        using SqlConnection connection = new("_connectionString");

        await connection
                .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, send_date, customer_id) VALUES (@message, @is_sent, @error_message, @send_date, @customer_id)",
                new
                {
                    context.Message.Message,
                    IsSent = false,
                    context.Message.ErrorMessage,
                    context.Message.SentDate,
                    context.Message.CustomerId
                });
    }
}