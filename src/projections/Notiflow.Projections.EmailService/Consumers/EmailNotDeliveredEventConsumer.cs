﻿namespace Notiflow.Projections.EmailService.Consumers;

internal class EmailNotDeliveredEventConsumer(
    IDbConnection connection,
    ILogger<EmailNotDeliveredEventConsumer> logger) : IConsumer<EmailNotDeliveredEvent>
{
    public async Task Consume(ConsumeContext<EmailNotDeliveredEvent> context)
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
                is_sent = false,
                is_body_html = context.Message.IsBodyHtml,
                error_message = context.Message.ErrorMessage,
                sent_date = context.Message.SentDate,
                customer_id = customerId
            });

            await connection
                  .ExecuteAsync(
                   "insert into emailhistory (recipients, cc, bcc, subject, body, is_sent, is_body_html, error_message, sent_date, customer_id) values (@recipients, @cc, @bcc, @subject, @body, @is_sent, @is_body_html, @error_message, @sent_date, @customer_id)",
                   emailHistories,
                   transaction);

            transaction.Commit();

            logger.LogInformation("Failed e-mail sending information is saved in the database.");

        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to save failed e-mail sending information to database.");

            transaction.Rollback();
            throw;
        }
    }
}
