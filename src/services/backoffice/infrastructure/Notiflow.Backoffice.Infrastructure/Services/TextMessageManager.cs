namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class TextMessageManager : ITextMessageService
{
    private static readonly Random Random = new();

    public Task<bool> SendTextMessageAsync(string phoneNumber, string message)
    {
        return Task.FromResult(Random.NextDouble() < 0.5);
    }

    public Task<bool> SendTextMessageAsync(IEnumerable<string> phoneNumber, string message)
    {
        return Task.FromResult(Random.NextDouble() < 0.5);
    }
}
