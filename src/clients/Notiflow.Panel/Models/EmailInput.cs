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
    public EmailInputValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleForEach(p => p.CcAddresses)
            .Email(localizer[ValidationErrorMessage.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(localizer[ValidationErrorMessage.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Body).NotNullAndNotEmpty(localizer[ValidationErrorMessage.EMAIL_BODY]);

        RuleFor(p => p.Subject)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.EMAIL_SUBJECT])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.EMAIL_SUBJECT]);
    }
}
