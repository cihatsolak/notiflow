﻿namespace Notiflow.Projections.TextMessageService.Consumers;

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
        using NpgsqlConnection npgSqlConnection = new(_notiflowDbSetting.ConnectionString);

        try
        {
            await npgSqlConnection
                    .ExecuteAsync("insert into textmessagehistory (message, is_sent, error_message, sent_date, customer_id) values (@message, @is_sent, @error_message, @sent_date, @customer_id)",
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
        finally
        {
            await npgSqlConnection.CloseAsync();
            await npgSqlConnection.DisposeAsync();
        }
    }
}