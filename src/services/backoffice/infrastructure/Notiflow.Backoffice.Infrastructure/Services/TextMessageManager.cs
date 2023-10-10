namespace Notiflow.Backoffice.Infrastructure.Services;

internal sealed class TextMessageManager : ITextMessageService
{
    private readonly ILogger<TextMessageManager> _logger;

    public TextMessageManager(ILogger<TextMessageManager> logger)
    {
        _logger = logger;
    }

    public Task<bool> SendTextMessageAsync(List<string> phoneNumbers, string message, CancellationToken cancellationToken)
    {
        if (Random.Shared.NextDouble() < 0.75)
        {
            return Task.FromResult(true);
        }

        _logger.LogWarning("Messages could not be sent to phone numbers {phoneNumbers}", phoneNumbers);

        return Task.FromResult(true);
    }
}
