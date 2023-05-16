namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.SendMultiple;

public sealed class SendMultipleNotificationCommandValidator : AbstractValidator<SendMultipleNotificationCommand>
{
    public SendMultipleNotificationCommandValidator()
    {
        RuleFor(p => p.Title).NotNullAndNotEmpty("-1");
    }
}
