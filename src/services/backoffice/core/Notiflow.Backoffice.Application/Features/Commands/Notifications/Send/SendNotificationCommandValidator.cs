namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ResultState.CUSTOMER_ID]);

        RuleFor(p => p.Title)
            .NotNullAndNotEmpty(localizer[ResultState.NOTIFICATION_TITLE])
            .MaximumLength(300).WithMessage(localizer[ResultState.NOTIFICATION_TITLE]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ResultState.NOTIFICATION_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ResultState.NOTIFICATION_MESSAGE]);

        RuleFor(p => p.ImageUrl)
           .NotNullAndNotEmpty(localizer[ResultState.NOTIFICATION_IMAGE_URL])
           .MaximumLength(300).WithMessage(localizer[ResultState.NOTIFICATION_IMAGE_URL]);
    }
}
