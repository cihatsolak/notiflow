using Notiflow.Common.MessageBroker.Events.TextMessage;

namespace Notiflow.Projections.TextMessageService.Consumers;

public sealed class TextMessageNotDeliveredEventConsumer : IConsumer<TextMessageNotDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<TextMessageNotDeliveredEventConsumer> _logger;

    public TextMessageNotDeliveredEventConsumer(
        IOptions<NotiflowDbSetting> notiflowDbSetting,
        ILogger<TextMessageNotDeliveredEventConsumer> logger)
    {
        _notiflowDbSetting = notiflowDbSetting.Value;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<TextMessageNotDeliveredEvent> context)
    {
        try
        {
            using NpgsqlConnection npgsqlConnection = new(_notiflowDbSetting.ConnectionString);

            await npgsqlConnection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, sent_date, customer_id) VALUES (@message, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        message = context.Message.Message,
                        is_sent = false,
                        error_message = context.Message.ErrorMessage,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The message that could not be sent has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The not sent message could not be saved to the database.");
        }
    }
}