namespace Notiflow.Backoffice.Application.Features.Commands.Emails;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    private const int EMAIL_SUBJECT_MAX_LENGTH = 300;

    public SendEmailCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);
        RuleFor(p => p.Body).Ensure(localizer[ValidationErrorMessage.EMAIL_BODY]);
        RuleFor(p => p.Subject).Ensure(localizer[ValidationErrorMessage.EMAIL_SUBJECT], EMAIL_SUBJECT_MAX_LENGTH);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });
    }
}