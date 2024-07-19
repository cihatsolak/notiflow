namespace Notiflow.Projections.EmailService.Consumers;

public sealed class EmailDeliveredEventConsumer(
    IDbConnection connection,
    ILogger<EmailDeliveredEvent> logger) : IConsumer<EmailDeliveredEvent>
{
    public async Task Consume(ConsumeContext<EmailDeliveredEvent> context)
    {
        IDbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
       
        try
        {
            var emailHistories = context.Message.CustomerIds.Select(customerId => new
            {
                recipients = context.Message.Recipients.JoinWithSeparator(PunctuationChars.Comma),
                cc = context.Message.CcAddresses.JoinWithSeparator(PunctuationChars.Comma),
                bcc = context.Message.BccAddresses.JoinWithSeparator(PunctuationChars.Comma),
                subject = context.Message.Subject,
                body = context.Message.Body,
                is_sent = true,
                is_body_html = context.Message.IsBodyHtml,
                error_message = (string)null,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await connection
                .ExecuteAsync(
                 "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                 emailHistories,
                 transaction);
           
            transaction.Commit();

            logger.LogInformation("Successful e-mail sending information has been saved in the database.");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to save successful e-mail sending information to database.");

            transaction.Rollback();
            throw;
        }
    }
}
