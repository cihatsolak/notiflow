namespace Notiflow.Projections.EmailService.Consumers;

internal class EmailNotDeliveredEventConsumer : IConsumer<EmailNotDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<EmailNotDeliveredEventConsumer> _logger;

    public EmailNotDeliveredEventConsumer(
        NotiflowDbSetting notiflowDbSetting, 
        ILogger<EmailNotDeliveredEventConsumer> logger)
    {
        _notiflowDbSetting = notiflowDbSetting;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EmailNotDeliveredEvent> context)
    {
        using NpgsqlConnection npgSqlConnection = new(_notiflowDbSetting.ConnectionString);
        using NpgsqlTransaction transaction = await npgSqlConnection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            foreach (var customerId in context.Message.CustomerIds)
            {
                await npgSqlConnection
                   .ExecuteAsync("insert into emailhistory (cc, bcc, subject, body, is_sent, error_message, sent_date, customer_id) values (@cc, @bcc, @subject, @body, @is_sent, @error_message, @sent_date, @customer_id)",
                   new
                   {
                       cc = context.Message.CcAddresses,
                       bcc = context.Message.BccAddresses,
                       body = context.Message.Body,
                       is_sent = false,
                       error_message = context.Message.ErrorMessage,
                       sent_date = context.Message.SentDate,
                       customer_id = customerId
                   }, transaction);
            }

            await transaction.CommitAsync();

            _logger.LogInformation("The sent notification has been saved in the database.");

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "The sent notification could not be saved to the database.");

            await transaction.RollbackAsync();
        }
        finally
        {
            await transaction.DisposeAsync();
            await npgSqlConnection.CloseAsync();
            await npgSqlConnection.DisposeAsync();
        }
    }
}
