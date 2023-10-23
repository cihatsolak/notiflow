namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record ScheduledTextMessageEvent
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
}