namespace Notiflow.IdentityServer.Core.Entities.Users;

public class UserOutbox
{
    public Guid IdempotentToken { get; set; }
    public string MessageType { get; set; }
    public string Payload { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public bool IsProcessed { get; set; }
}