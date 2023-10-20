using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;

public sealed class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    public SendNotificationCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorCodes.CUSTOMER_ID]);

        RuleFor(p => p.Title)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.NOTIFICATION_TITLE])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.NOTIFICATION_TITLE]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorCodes.NOTIFICATION_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.NOTIFICATION_MESSAGE]);

        RuleFor(p => p.ImageUrl)
           .NotNullAndNotEmpty(localizer[ValidationErrorCodes.NOTIFICATION_IMAGE_URL])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.NOTIFICATION_IMAGE_URL]);
    }
}
