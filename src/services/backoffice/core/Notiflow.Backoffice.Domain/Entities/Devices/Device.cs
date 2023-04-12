namespace Notiflow.Backoffice.Domain.Entities.Devices;

public class Device : BaseHistoricalEntity
{
    public OSVersion OSVersion { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
