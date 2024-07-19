namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class EmailManager(
    IRedisService redisService,
    ILogger<EmailManager> logger) : IEmailService
{
    public async Task<bool> SendAsync(EmailRequest request, CancellationToken cancellationToken)
    {
        var tenantApplication = await redisService.HashGetAsync<TenantApplicationCacheModel>(TenantCacheKeyFactory.Generate(CacheKeys.TENANT_INFO), CacheKeys.TENANT_APPS_CONFIG)
            ?? throw new TenantException("The tenant's application information could not be found.");

        using SmtpClient smtpClient = new();
        smtpClient.Host = tenantApplication.MailSmtpHost;
        smtpClient.Port = tenantApplication.MailSmtpPort;
        smtpClient.Timeout = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
        smtpClient.EnableSsl = tenantApplication.MailSmtpIsUseSsl;

        using MailMessage mailMessage = new();
        mailMessage.From = new MailAddress(tenantApplication.MailFromAddress, tenantApplication.MailFromName, Encoding.UTF8);
        mailMessage.Subject = request.Subject;
        mailMessage.SubjectEncoding = Encoding.UTF8;
        mailMessage.IsBodyHtml = request.IsBodyHtml;
        mailMessage.Body = request.Body;
        mailMessage.BodyEncoding = Encoding.UTF8;
        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.None;


        foreach (var emailAddress in request.Recipients.OrEmptyIfNull())
        {
            mailMessage.To.Add(emailAddress);
        }

        foreach (var emailAddress in request.CcAddresses.OrEmptyIfNull())
        {
            mailMessage.CC.Add(emailAddress);
        }

        foreach (var emailAddress in request.BccAddresses.OrEmptyIfNull())
        {
            mailMessage.Bcc.Add(emailAddress);
        }

        try
        {
            await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Email sending failed.");
        }

        return default;
    }
}
