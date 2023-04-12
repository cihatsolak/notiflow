namespace Notiflow.Backoffice.Domain.Entities.Histories;

public sealed class NotificationHistory : BaseEntity
{
    public string Title { get; set; }
    public string Message { get; set; }
    public bool IsSent { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}