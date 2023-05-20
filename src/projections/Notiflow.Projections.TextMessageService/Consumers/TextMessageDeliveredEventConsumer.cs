namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageDeliveredEventConsumer : IConsumer<TextMessageDeliveredEvent>
{
    public async Task Consume(ConsumeContext<TextMessageDeliveredEvent> context)
    {
        using SqlConnection connection = new("_connectionString");

        await connection
                .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, send_date, customer_id) VALUES (@message, @is_sent, @error_message, @send_date, @customer_id)",
                new
                {
                    context.Message.Message,
                    IsSent = true,
                    ErrorMessage = DBNull.Value,
                    context.Message.SentDate,
                    context.Message.CustomerId
                });
    }
}
