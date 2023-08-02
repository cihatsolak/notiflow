namespace Notiflow.Backoffice.Application.Models;

internal class NotificationResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
    public Guid SecretIdentity { get; set; }

    public NotificationResult()
    {
        SecretIdentity = Guid.Empty;
    }
}
