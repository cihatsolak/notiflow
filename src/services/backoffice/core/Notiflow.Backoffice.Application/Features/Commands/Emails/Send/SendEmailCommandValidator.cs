namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ResultState.CUSTOMER_ID]);

        RuleForEach(p => p.CcAddresses)
            .Email(localizer[ResultState.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(localizer[ResultState.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Body).NotNullAndNotEmpty(localizer[ResultState.EMAIL_BODY]);

        RuleFor(p => p.Subject)
            .NotNullAndNotEmpty(localizer[ResultState.EMAIL_SUBJECT])
            .MaximumLength(300).WithMessage(localizer[ResultState.EMAIL_SUBJECT]);
    }
}