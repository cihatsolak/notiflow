namespace Notiflow.Projections.NotificationService.Consumers;

public sealed class NotificationDeliveredEventConsumer : IConsumer<NotificationDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<NotificationDeliveredEventConsumer> _logger;

    public NotificationDeliveredEventConsumer(
        IOptions<NotiflowDbSetting> notiflowDbSetting,
        ILogger<NotificationDeliveredEventConsumer> logger)
    {
        _notiflowDbSetting = notiflowDbSetting.Value;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationDeliveredEvent> context)
    {
        try
        {
            using NpgsqlConnection npgsqlConnection = new(_notiflowDbSetting.ConnectionString);

            await npgsqlConnection
                    .ExecuteAsync("insert into notificationhistory (title, message, is_sent, error_message, sent_date, customer_id) VALUES (@title, @message, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        title = context.Message.Title,
                        message = context.Message.Message,
                        is_sent = true,
                        error_message = (string)null,
                        sent_date = context.Message.SentDate,
                        customer_id = context.Message.CustomerId
                    });

            _logger.LogInformation("The sent notification has been saved in the database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The sent notification could not be saved to the database.");
        }
    }
}
