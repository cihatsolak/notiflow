namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        RuleFor(p => p.Body).NotNullAndNotEmpty(localizer[ValidationErrorMessage.EMAIL_BODY]);

        RuleFor(p => p.Subject)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.EMAIL_SUBJECT])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.EMAIL_SUBJECT]);
    }
}