using Notiflow.Common.MessageBroker.Events.TextMessage;

namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageDeliveredEventConsumer : IConsumer<TextMessageDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<TextMessageDeliveredEventConsumer> _logger;

    public TextMessageDeliveredEventConsumer(
        IOptions<NotiflowDbSetting> notiflowDbSetting,
        ILogger<TextMessageDeliveredEventConsumer> logger)
    {
        _notiflowDbSetting = notiflowDbSetting.Value;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TextMessageDeliveredEvent> context)
    {
        try
        {
            using NpgsqlConnection npgsqlConnection = new(_notiflowDbSetting.ConnectionString);

            await npgsqlConnection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, sent_date, customer_id) VALUES (@message, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        message = context.Message.Message,
                        is_sent = true,
                        error_message = (string)null,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The sent message has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The sent message could not be saved to the database.");
        }
    }
}
