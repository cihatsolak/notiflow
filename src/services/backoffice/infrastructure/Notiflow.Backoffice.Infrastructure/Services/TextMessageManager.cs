namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class TextMessageManager : ITextMessageService
{
    private static readonly Random Random = new();

    public Task<bool> SendTextMessageAsync(List<string> phoneNumbers, string message, CancellationToken cancellationToken)
    {
        return Task.FromResult(Random.NextDouble() < 0.5);
    }
}
