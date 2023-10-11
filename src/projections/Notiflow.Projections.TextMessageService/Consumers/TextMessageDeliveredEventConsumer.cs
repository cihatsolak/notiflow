namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageDeliveredEventConsumer : IConsumer<TextMessageDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<TextMessageDeliveredEventConsumer> _logger;

    public TextMessageDeliveredEventConsumer(
        IDbConnection connection, 
        ILogger<TextMessageDeliveredEventConsumer> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TextMessageDeliveredEvent> context)
    {
        try
        {
            var textMessageHistories = context.Message.CustomerIds.Select(customerId => new
            {
                message = context.Message.Message,
                is_sent = true,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await _connection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, sent_date, customer_id) values (@message, @is_sent, @sent_date, @customer_id)",
                     textMessageHistories);

            _logger.LogInformation("The sent messages has been saved in the database.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "The sent messages could not be saved to the database.");
            throw;
        }
    }
}
