namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class EmailManager : IEmailService
{
    private readonly ILogger<EmailManager> _logger;

    public EmailManager(ILogger<EmailManager> logger)
    {
        _logger = logger;
    }

    public async Task<bool> SendAsync()
    {
        using SmtpClient smtpClient = new();
        smtpClient.Host = "";
        smtpClient.Port = 25;
        smtpClient.Timeout = (int)TimeSpan.FromMinutes(2).TotalMilliseconds;

        using MailMessage mailMessage = new();
        mailMessage.From = new MailAddress("");
        mailMessage.Subject = "";
        mailMessage.SubjectEncoding = Encoding.UTF8;
        mailMessage.IsBodyHtml = true;
        mailMessage.Body = "";
        mailMessage.BodyEncoding = Encoding.UTF8;
        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;

       
        foreach (var emailAddress in Enumerable.Empty<string>())
        {
            mailMessage.To.Add(emailAddress);
        }

        foreach (var emailAddress in Enumerable.Empty<string>())
        {
            mailMessage.CC.Add(emailAddress);
        }

        foreach (var emailAddress in Enumerable.Empty<string>())
        {
            mailMessage.Bcc.Add(emailAddress);
        }

        try
        {
            await smtpClient.SendMailAsync(mailMessage);

            return true;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Email sending failed.");

            return default;
        }
        finally
        {
            smtpClient.Dispose();
            mailMessage.Dispose();
        }
    }
}
