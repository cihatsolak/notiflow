namespace Notiflow.Panel.Models;

public sealed record EmailInput
{
    [Display(Name = "input.email.body.resource")]
    [DataType(DataType.MultilineText)]
    public required string Body { get; init; }

    [Display(Name = "input.subject.resource")]
    [DataType(DataType.Text)]
    public required string Subject { get; init; }

    [Display(Name = "input.customer.numbers.resource")]
    [DataType(DataType.Text)]
    public required List<int> CustomerIds { get; init; }

    [Display(Name = "input.email.ccaddress.resource")]
    [DataType(DataType.EmailAddress)]
    public required List<string> CcAddresses { get; init; }

    [Display(Name = "input.email.bccaddress.resource")]
    [DataType(DataType.EmailAddress)]
    public required List<string> BccAddresses { get; init; }

    [Display(Name = "input.email.isbodyhtml.resource")]
    public required bool IsBodyHtml { get; init; }
}

public sealed class EmailInputValidator : AbstractValidator<EmailInput>
{
    public EmailInputValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleForEach(p => p.CcAddresses)
            .Email(FluentVld.Errors.EMAIL).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(FluentVld.Errors.EMAIL).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Body).Ensure(FluentVld.Errors.EMAIL_BODY);

        RuleFor(p => p.Subject)
            .Ensure(FluentVld.Errors.EMAIL_SUBJECT)
            .MaximumLength(300).WithMessage(FluentVld.Errors.EMAIL_SUBJECT);
    }
}
