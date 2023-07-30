﻿namespace Notiflow.Common.MessageBroker.Events.TextMessage;

public class TextMessageNotDeliveredEvent
{
    public TextMessageNotDeliveredEvent()
    {
        SentDate = DateTime.Now;
        ErrorMessage = "The message could not be sent for an unknown reason.";
    }

    public int CustomerId { get; set; }
    public string Message { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }
}