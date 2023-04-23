namespace Notiflow.Backoffice.Domain.Entities.Users;

public class User: BaseHistoricalEntity
{
    public string Username { get; set; }
    public string Password { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
