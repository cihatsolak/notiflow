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
        using NpgsqlConnection npgsqlConnection = new(_notiflowDbSetting.ConnectionString);
        using NpgsqlTransaction npgsqlTransaction = await npgsqlConnection.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        try
        {
            var emailHistories = context.Message.CustomerIds.Select(customerId => new
            {
                recipients = CombineWithComma(context.Message.Recipients),
                cc = CombineWithComma(context.Message.CcAddresses),
                bcc = CombineWithComma(context.Message.BccAddresses),
                subject = context.Message.Subject,
                body = context.Message.Body,
                is_sent = false,
                is_body_html = context.Message.IsBodyHtml,
                error_message = context.Message.ErrorMessage,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await npgsqlConnection
                  .ExecuteAsync(
                   "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                   emailHistories,
                   npgsqlTransaction);

            await npgsqlTransaction.CommitAsync();

            _logger.LogInformation("Failed e-mail sending information is saved in the database.");

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to save failed e-mail sending information to database.");

            await npgsqlTransaction.RollbackAsync();
        }
        finally
        {
            await npgsqlTransaction.DisposeAsync();
            await npgsqlConnection.CloseAsync();
            await npgsqlConnection.DisposeAsync();
        }
    }

    private static string CombineWithComma(List<string> list) => string.Join(";", list);
}
