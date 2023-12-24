namespace Notiflow.Backoffice.Application.Models.Notifications;

public class NotificationResult
{
    public bool Succeeded { get; set; }
    public string ErrorMessage { get; set; }
    public Guid SecretIdentity { get; set; }

    public NotificationResult()
    {
        SecretIdentity = Guid.Empty;
    }

    public NotificationResult(string errorMessage) : this()
    {
        ErrorMessage = errorMessage;
        Succeeded = false;
    }

    public NotificationResult(bool succeeded, string errorMessage) : this()
    {
        ErrorMessage = errorMessage;
        Succeeded = succeeded;
    }
}
