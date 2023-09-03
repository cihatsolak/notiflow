namespace Notiflow.Backoffice.Domain.Entities.Devices;

public class Device : BaseHistoricalEntity<int>
{
    public OSVersion OSVersion { get; set; }
    public string Code { get; set; }
    public string Token { get; set; }
    public CloudMessagePlatform CloudMessagePlatform { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
}
