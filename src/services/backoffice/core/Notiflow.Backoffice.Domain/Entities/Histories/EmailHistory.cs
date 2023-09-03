﻿namespace Notiflow.Backoffice.Domain.Entities.Histories;

public sealed class EmailHistory : BaseEntity<int>
{
    public string Recipients { get; set; }
    public string Cc { get; set; }
    public string Bcc { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsSent { get; set; }
    public bool IsBodyHtml { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}