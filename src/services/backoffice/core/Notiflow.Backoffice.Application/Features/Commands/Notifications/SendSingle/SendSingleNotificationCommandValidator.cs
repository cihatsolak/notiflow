namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendSingleNotificationCommandValidator : AbstractValidator<SendSingleNotificationCommand>
{
    public SendSingleNotificationCommandValidator()
    {
        RuleFor(p => p.Title).NotNullAndNotEmpty("errorCode");
    }
}
