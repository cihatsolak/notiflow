namespace Notiflow.Projections.EmailService.Consumers;

public sealed class EmailDeliveredEventConsumer : IConsumer<EmailDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<EmailDeliveredEvent> _logger;

    public EmailDeliveredEventConsumer(
        IDbConnection connection,
        ILogger<EmailDeliveredEvent> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EmailDeliveredEvent> context)
    {
        IDbTransaction transaction = _connection.BeginTransaction(IsolationLevel.ReadCommitted);
       
        try
        {
            var emailHistories = context.Message.CustomerIds.Select(customerId => new
            {
                recipients = context.Message.Recipients.ToJoinSeparator(PunctuationChars.Comma),
                cc = context.Message.CcAddresses.ToJoinSeparator(PunctuationChars.Comma),
                bcc = context.Message.BccAddresses.ToJoinSeparator(PunctuationChars.Comma),
                subject = context.Message.Subject,
                body = context.Message.Body,
                is_sent = true,
                is_body_html = context.Message.IsBodyHtml,
                error_message = (string)null,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await _connection
                .ExecuteAsync(
                 "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                 emailHistories,
                 transaction);
           
            transaction.Commit();

            _logger.LogInformation("Successful e-mail sending information has been saved in the database.");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to save successful e-mail sending information to database.");

            transaction.Rollback();
            throw;
        }
    }
}
