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
        using NpgsqlConnection npgSqlConnection = new(_notiflowDbSetting.ConnectionString);

        try
        {
            var textMessageHistories = context.Message.CustomerIds.Select(customerId => new
            {
                message = context.Message.Message,
                is_sent = true,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await npgSqlConnection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, sent_date, customer_id) values (@message, @is_sent, @sent_date, @customer_id)",
                     textMessageHistories);

            _logger.LogInformation("The sent messages has been saved in the database.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "The sent messages could not be saved to the database.");
        }
        finally
        {
            await npgSqlConnection.CloseAsync();
            await npgSqlConnection.DisposeAsync();
        }
    }
}
