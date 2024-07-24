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

    internal ScheduledEmail CreateScheduledEmail()
    {
        return new ScheduledEmail
        {
            Data = new ScheduledEmailEvent(Body, Subject, CustomerIds, CcAddresses, BccAddresses, IsBodyHtml).ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{Date} {Time}", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal)
        };
    }
}

public sealed class ScheduleEmailRequestValidator : AbstractValidator<ScheduleEmailRequest>
{
    public ScheduleEmailRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Body).Ensure(localizer[ValidationErrorMessage.EMAIL_BODY]);

        RuleFor(p => p.Subject)
          .Ensure(localizer[ValidationErrorMessage.EMAIL_SUBJECT])
          .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.EMAIL_SUBJECT]);

        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(localizer[ValidationErrorMessage.EMAIL]);
        });

        RuleFor(p => p.Date)
            .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorMessage.DATE]);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorMessage.TIME]);
    }
}
