﻿namespace Notiflow.Backoffice.Infrastructure.Services;

public sealed class TextMessageManager : ITextMessageService
{
    public Task<bool> SendTextMessageAsync(string phoneNumber, string message)
    {
        return Task.FromResult(true);
    }

    public Task<bool> SendTextMessageAsync(IEnumerable<string> phoneNumber, string message)
    {
        return Task.FromResult(true);
    }
}
