namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record ScheduledTextMessageSendEvent
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
}