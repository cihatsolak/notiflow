﻿namespace Notiflow.Common.Events;

public class TextMessageDeliveredEvent
{
    public string CustomerId { get; set; }
    public string Message { get; set; }
    public DateTime SentDate { get; set; }
}
