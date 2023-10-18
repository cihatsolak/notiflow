namespace Notiflow.Common.MessageBroker.Events;

public static class RabbitQueueName
{
    public const string NOTIFICATION_DELIVERED_EVENT_QUEUE = "notification-delivered-event-queue";
    public const string NOTIFICATION_NOT_DELIVERED_EVENT_QUEUE = "notification-not-delivered-event-queue";

    public const string TEXT_MESSAGE_DELIVERED_EVENT_QUEUE = "text-message-delivered-event-queue";
    public const string TEXT_MESSAGE_NOT_DELIVERED_EVENT_QUEUE = "text-message-not-delivered-event-queue";

    public const string EMAIL_DELIVERED_EVENT_QUEUE = "email-delivered-event-queue";
    public const string EMAIL_NOT_DELIVERED_EVENT_QUEUE = "email-not-delivered-event-queue";

    public const string SCHEDULED_TEXT_MESSAGE_SEND = "scheduled-text-message-send-queue";
    public const string SCHEDULED_NOTIFICATIN_SEND = "scheduled-notification-send-queue";
}
