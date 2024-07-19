namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class TextMessageManager(ILogger<TextMessageManager> logger) : ITextMessageService
{
    public Task<bool> SendTextMessageAsync(string phoneNumber, string message, CancellationToken cancellationToken)
    {
        if (Random.Shared.NextDouble() < 0.75)
        {
            return Task.FromResult(true);
        }

        logger.LogWarning("Messages could not be sent to phone numbers {phoneNumber}", phoneNumber);

        return Task.FromResult(true);
    }

    public Task<bool> SendTextMessageAsync(List<string> phoneNumbers, string message, CancellationToken cancellationToken)
    {
        if (Random.Shared.NextDouble() < 0.75)
        {
            return Task.FromResult(true);
        }

        logger.LogWarning("Messages could not be sent to phone numbers {phoneNumbers}", phoneNumbers);

        return Task.FromResult(true);
    }
}
