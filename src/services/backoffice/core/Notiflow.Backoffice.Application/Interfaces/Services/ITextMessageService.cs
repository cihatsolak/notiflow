namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface ITextMessageService
{
    Task<bool> SendTextMessageAsync(string phoneNumber, string message);
    Task<bool> SendTextMessageAsync(IEnumerable<string> phoneNumber, string message);
}
