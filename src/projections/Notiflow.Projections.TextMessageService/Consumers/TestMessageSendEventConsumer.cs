namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TestMessageSendEventConsumer : IConsumer<TestMessageSendEvent>
{
    public async Task Consume(ConsumeContext<TestMessageSendEvent> context)
    {
        using var connection = new SqlConnection("_connectionString");

        await connection
                .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, send_date, customer_id) VALUES (@message, @is_sent, @error_message, @send_date, @customer_id)",
                new
                {
                    context.Message.Message,
                    context.Message.IsSent,
                    context.Message.ErrorMessage,
                    context.Message.SentDate,
                    context.Message.CustomerId
                });
    }
}
