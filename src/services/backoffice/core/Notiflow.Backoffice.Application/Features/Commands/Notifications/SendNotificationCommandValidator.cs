namespace Notiflow.Backoffice.Application.Features.Commands.Notifications;

public sealed class SendNotificationCommandValidator : AbstractValidator<SendNotificationCommand>
{
    private const int NOTIFICATION_TITLE_MAX_LENGTH = 300;
    private const int NOTIFICATION_MESSAGE_MAX_LENGTH = 300;
    private const int NOTIFICATION_IMAGE_URL_MAX_LENGTH = 300;

    public SendNotificationCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);
        RuleFor(p => p.Title).Ensure(localizer[ValidationErrorMessage.NOTIFICATION_TITLE], NOTIFICATION_TITLE_MAX_LENGTH);
        RuleFor(p => p.Message).Ensure(localizer[ValidationErrorMessage.NOTIFICATION_MESSAGE], NOTIFICATION_MESSAGE_MAX_LENGTH);
        RuleFor(p => p.ImageUrl).Url(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL], NOTIFICATION_IMAGE_URL_MAX_LENGTH);
    }
}
