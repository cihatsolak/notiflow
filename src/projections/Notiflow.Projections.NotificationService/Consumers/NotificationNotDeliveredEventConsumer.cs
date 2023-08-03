namespace Notiflow.Projections.NotificationService.Consumers;

internal class NotificationNotDeliveredEventConsumer : IConsumer<NotificationNotDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<NotificationNotDeliveredEventConsumer> _logger;

    public NotificationNotDeliveredEventConsumer(
        IOptions<NotiflowDbSetting> notiflowDbSetting,
        ILogger<NotificationNotDeliveredEventConsumer> logger)
    {
        _notiflowDbSetting = notiflowDbSetting.Value;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationNotDeliveredEvent> context)
    {
        using NpgsqlConnection npgSqlConnection = new(_notiflowDbSetting.ConnectionString);

        try
        {
            await npgSqlConnection
                    .ExecuteAsync("insert into notificationhistory (title, message, sender_identity, is_sent, error_message, sent_date, customer_id) values (@title, @message, @sender_identity, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        title = context.Message.Title,
                        message = context.Message.Message,
                        sender_identity = context.Message.SenderIdentity,
                        is_sent = false,
                        error_message = context.Message.ErrorMessage,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The notification that could not be sent has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The not sent notification could not be saved to the database.");
        }
        finally
        {
            await npgSqlConnection.CloseAsync();
            await npgSqlConnection.DisposeAsync();
        }
    }
}
