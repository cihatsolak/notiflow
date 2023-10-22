namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorCodes.CUSTOMER_ID]);

        RuleForEach(p => p.CcAddresses)
            .Email(localizer[ValidationErrorCodes.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(localizer[ValidationErrorCodes.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Body).NotNullAndNotEmpty(localizer[ValidationErrorCodes.EMAIL_BODY]);

        RuleFor(p => p.Subject)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.EMAIL_SUBJECT])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.EMAIL_SUBJECT]);
    }
}