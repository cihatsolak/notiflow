namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleForEach(p => p.CustomerIds)
            .InclusiveBetween(1, int.MaxValue).WithMessage(FluentValidationErrorCodes.CUSTOMER_ID);

        RuleForEach(p => p.CcAddresses)
            .Email(FluentValidationErrorCodes.EMAIL).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(FluentValidationErrorCodes.EMAIL).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Body).NotNullAndNotEmpty(FluentValidationErrorCodes.EMAIL_BODY);

        RuleFor(p => p.Subject)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.EMAIL_SUBJECT)
            .MaximumLength(300).WithMessage(FluentValidationErrorCodes.EMAIL_SUBJECT);
    }
}