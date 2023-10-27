namespace Notiflow.Schedule.Models;

public sealed record ScheduleTextMessageRequest
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
    public string Date { get; set; }
    public string Time { get; set; }
}

public sealed class ScheduleTextMessageRequestValidator : AbstractValidator<ScheduleTextMessageRequest>
{
    public ScheduleTextMessageRequestValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorCodes.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorCodes.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorCodes.TEXT_MESSAGE]);

        RuleFor(p => p.Date)
             .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
             .WithMessage(localizer[ValidationErrorCodes.DATE]);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorCodes.TIME]);
    }
}
