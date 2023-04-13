namespace Notiflow.Backoffice.Application.Commands.Notifications.SendNotification;

public sealed class SendNotificationRequestValidator : AbstractValidator<SendNotificationRequest>
{
    public SendNotificationRequestValidator()
    {
        RuleFor(p => p.Title).NotNullAndNotEmpty("errorCode");
    }
}
