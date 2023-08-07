namespace Notiflow.Backoffice.Application.Interfaces.Services;

public interface ITextMessageService
{
    Task<List<TextMessageResult>> SendTextMessageAsync(TextMessageRequest request, CancellationToken cancellationToken);
}
