﻿namespace Notiflow.Projections.NotificationService.Consumers;

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
        try
        {
            using NpgsqlConnection npgsqlConnection = new(_notiflowDbSetting.ConnectionString);

            await npgsqlConnection
                    .ExecuteAsync("insert into notificationhistory (title, message, is_sent, error_message, sent_date, customer_id) VALUES (@title, @message, @is_sent, @error_message, @sent_date, @customer_id)",
                    new
                    {
                        title = context.Message.Title,
                        message = context.Message.Message,
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
    }
}
