namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class TextMessageManager : ITextMessageService
{
    private static readonly Random Random = new();

    public Task<List<TextMessageResult>> SendTextMessageAsync(TextMessageRequest request, CancellationToken cancellationToken)
    {
        var textMessageResult = request.PhoneNumbers.Select(phoneNumber => new TextMessageResult
        {
            PhoneNumber = phoneNumber,
            IsSent = Random.NextDouble() < 0.5
        }).ToList();

        return Task.FromResult(textMessageResult);
    }
}
