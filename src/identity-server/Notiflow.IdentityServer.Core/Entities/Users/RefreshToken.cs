namespace Notiflow.IdentityServer.Core.Entities.Users;

public class RefreshToken : BaseEntity<int>
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
