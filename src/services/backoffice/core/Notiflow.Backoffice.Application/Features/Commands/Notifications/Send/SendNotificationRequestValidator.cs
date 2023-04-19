namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationRequestValidator : AbstractValidator<SendNotificationRequest>
{
    public SendNotificationRequestValidator()
    {
        RuleFor(p => p.Title).NotNullAndNotEmpty("errorCode");
    }
}
