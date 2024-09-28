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
    public ScheduleEmailRequestValidator()
    {
        RuleFor(p => p.Body).Ensure(FluentVld.Errors.EMAIL_BODY);

        RuleFor(p => p.Subject)
          .Ensure(FluentVld.Errors.EMAIL_SUBJECT)
          .MaximumLength(300).WithMessage(FluentVld.Errors.EMAIL_SUBJECT);

        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        When(p => !p.CcAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.CcAddresses).Email(FluentVld.Errors.EMAIL);
        });

        When(p => !p.BccAddresses.IsNullOrNotAny(), () =>
        {
            RuleForEach(p => p.BccAddresses).Email(FluentVld.Errors.EMAIL);
        });

        RuleFor(p => p.Date)
            .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(FluentVld.Errors.DATE);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(FluentVld.Errors.TIME);
    }
}
