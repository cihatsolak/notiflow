namespace Notiflow.IdentityServer.Core.Entities.Users;

public class User : BaseHistoricalEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Uri ProfilePhoto { get; set; }

    public RefreshToken RefreshToken { get; set; }

    public int TenantId { get; set; }
    public Tenant Tenant { get; set; }
}
