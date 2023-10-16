namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public sealed record TextMessageSendingPlannedEvent
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
    public DateTime DeliveryDate { get; set; }
}
