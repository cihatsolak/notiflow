namespace Notiflow.Common.MessageBroker.Events;

public static class RabbitQueueName
{
    public const string NOTIFICATION_DELIVERED_EVENT_QUEUE = "notification-delivered-event-queue";
    public const string NOTIFICATION_NOT_DELIVERED_EVENT_QUEUE = "notification-not-delivered-event-queue";

    public const string TEXT_MESSAGE_DELIVERED_EVENT_QUEUE = "text-message-delivered-event-queue";
    public const string TEXT_MESSAGE_NOT_DELIVERED_EVENT_QUEUE = "text-message-not-delivered-event-queue";
}
