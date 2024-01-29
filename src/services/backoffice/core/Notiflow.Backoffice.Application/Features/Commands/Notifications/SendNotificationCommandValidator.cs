namespace Notiflow.Backoffice.Application.Features.Commands.Notifications;

public sealed class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Title)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.NOTIFICATION_TITLE])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_TITLE]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.NOTIFICATION_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_MESSAGE]);

        RuleFor(p => p.ImageUrl)
         .Url(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL])
         .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL]);
    }
}
