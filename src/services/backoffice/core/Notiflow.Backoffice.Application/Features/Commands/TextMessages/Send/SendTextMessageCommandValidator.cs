namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;

public sealed class SendTextMessageCommandValidator : AbstractValidator<SendTextMessageCommand>
{
    public SendTextMessageCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.TEXT_MESSAGE]);
    }
}