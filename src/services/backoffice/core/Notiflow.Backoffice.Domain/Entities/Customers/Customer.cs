namespace Notiflow.Backoffice.Domain.Entities.Customers;

public class Customer : BaseHistoricalSoftDeleteEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public MarriageStatus MarriageStatus { get; set; }

    public Device Device { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }

    public ICollection<NotificationHistory> NotificationHistories { get; set; }
}
