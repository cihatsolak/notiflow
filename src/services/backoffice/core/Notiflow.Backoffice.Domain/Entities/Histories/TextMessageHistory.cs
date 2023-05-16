namespace Notiflow.Backoffice.Domain.Entities.Histories;

public class TextMessageHistory : BaseEntity
{
    public string Message { get; set; }
    public bool IsSent { get; set; }
    public string ErrorMessage { get; set; }
    public DateTime SentDate { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
