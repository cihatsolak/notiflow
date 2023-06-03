namespace Notiflow.Common.MessageBroker.Events;

public static class SharedRabbitQueueName
{
    public const string NOTIFICATION_DELIVERED_EVENT_QUEUE = "notification-delivered-event-queue";
    public const string NOTIFICATION_NOT_DELIVERED_EVENT_QUEUE = "notification-not-delivered-event-queue";
}
