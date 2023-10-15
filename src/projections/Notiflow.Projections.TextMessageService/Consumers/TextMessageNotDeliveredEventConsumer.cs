﻿namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageNotDeliveredEventConsumer : IConsumer<TextMessageNotDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<TextMessageDeliveredEventConsumer> _logger;

    public TextMessageNotDeliveredEventConsumer(
        IDbConnection connection,
        ILogger<TextMessageDeliveredEventConsumer> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TextMessageNotDeliveredEvent> context)
    {
        try
        {
            var textMessageHistories = context.Message.CustomerIds.Select(customerId => new
            {
                message = context.Message.Message,
                is_sent = false,
                error_message = context.Message.ErrorMessage,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await _connection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, sent_date, customer_id) values (@message, @is_sent, @error_message, @sent_date, @customer_id)",
                    textMessageHistories);

            _logger.LogInformation("The messages that could not be sent has been saved in the database.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "The not sent messages could not be saved to the database.");
            throw;
        }
    }
}