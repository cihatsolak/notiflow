namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator()
    {
        RuleFor(p => p.Title).NotNullAndNotEmpty("errorCode");
    }
}
