namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface ITextMessageService
{
    Task<bool> SendTextMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken);
    Task<bool> SendTextMessageAsync(List<string> phoneNumbers, string message, CancellationToken cancellationToken);
}
