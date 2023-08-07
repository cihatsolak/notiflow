namespace Notiflow.Backoffice.Application.Features.Commands.Emails.Send;

public sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        RuleForEach(p => p.CustomerIds).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleForEach(p => p.CcAddresses).Email("errorMessage").When(p => !p.CcAddresses.IsNullOrNotAny());
        RuleForEach(p => p.BccAddresses).Email("errorMessage").When(p => !p.CcAddresses.IsNullOrNotAny());
        RuleFor(p => p.Subject).Length(2, 300);
    }
}