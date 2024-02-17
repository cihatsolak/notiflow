namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface IEmailService
{
    Task<bool> SendAsync(EmailRequest request, CancellationToken cancellationToken);
}
