namespace Notiflow.Backoffice.Domain.Entities.Notifications;

public sealed class NotificationHistory : BaseEntity
{
    public string Title { get; set; }
    public string Body { get; set; }
    public bool IsSent { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime CreatedDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}