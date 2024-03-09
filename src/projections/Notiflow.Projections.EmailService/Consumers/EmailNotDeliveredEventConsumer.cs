namespace Notiflow.Projections.EmailService.Consumers;

internal class EmailNotDeliveredEventConsumer : IConsumer<EmailNotDeliveredEvent>
{
    private readonly IDbConnection _connection;
    private readonly ILogger<EmailNotDeliveredEventConsumer> _logger;

    public EmailNotDeliveredEventConsumer(
        IDbConnection connection,
        ILogger<EmailNotDeliveredEventConsumer> logger)
    {
        _connection = connection;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<EmailNotDeliveredEvent> context)
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
                is_sent = false,
                is_body_html = context.Message.IsBodyHtml,
                error_message = context.Message.ErrorMessage,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await _connection
                  .ExecuteAsync(
                   "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                   emailHistories,
                   transaction);

            transaction.Commit();

            _logger.LogInformation("Failed e-mail sending information is saved in the database.");

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to save failed e-mail sending information to database.");

            transaction.Rollback();
            throw;
        }
    }
}
