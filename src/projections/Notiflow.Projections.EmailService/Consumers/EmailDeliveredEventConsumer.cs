namespace Notiflow.Projections.EmailService.Consumers;

public sealed class EmailDeliveredEventConsumer : IConsumer<EmailDeliveredEvent>
{
    private readonly NotiflowDbSetting _notiflowDbSetting;
    private readonly ILogger<EmailDeliveredEvent> _logger;

    public EmailDeliveredEventConsumer(
        NotiflowDbSetting notiflowDbSetting, 
        ILogger<EmailDeliveredEvent> logger)
    {
        _notiflowDbSetting = notiflowDbSetting;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EmailDeliveredEvent> context)
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
                is_sent = true,
                is_body_html = context.Message.IsBodyHtml,
                error_message = (string)null,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await npgsqlConnection
                .ExecuteAsync(
                 "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                 emailHistories,
                 npgsqlTransaction);

            await npgsqlTransaction.CommitAsync();

            _logger.LogInformation("Successful e-mail sending information has been saved in the database.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to save successful e-mail sending information to database.");

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
