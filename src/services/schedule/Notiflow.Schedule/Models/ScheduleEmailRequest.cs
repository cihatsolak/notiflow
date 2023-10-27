namespace Notiflow.Schedule.Models;

public sealed record ScheduleEmailRequest
{
    public required string Body { get; init; }
    public required string Subject { get; init; }
    public required List<int> CustomerIds { get; init; }
    public required List<string> CcAddresses { get; init; }
    public required List<string> BccAddresses { get; init; }
    public required bool IsBodyHtml { get; init; }
    public required string Date { get; init; }
    public required string Time { get; init; }
}

public sealed class ScheduleEmailRequestValidator : AbstractValidator<ScheduleEmailRequest>
{
    public ScheduleEmailRequestValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Body).NotNullAndNotEmpty(localizer[ValidationErrorCodes.EMAIL_BODY]);

        RuleFor(p => p.Subject)
          .NotNullAndNotEmpty(localizer[ValidationErrorCodes.EMAIL_SUBJECT])
          .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.EMAIL_SUBJECT]);

        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorCodes.CUSTOMER_ID]);

        RuleForEach(p => p.CcAddresses)
            .Email(localizer[ValidationErrorCodes.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleForEach(p => p.BccAddresses)
            .Email(localizer[ValidationErrorCodes.EMAIL]).When(p => !p.CcAddresses.IsNullOrNotAny());

        RuleFor(p => p.Date)
            .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorCodes.DATE]);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorCodes.TIME]);
    }
}
