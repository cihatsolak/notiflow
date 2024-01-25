namespace Notiflow.Schedule.Models;

public sealed record ScheduleTextMessageRequest
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
    public string Date { get; init; }
    public string Time { get; init; }

    internal ScheduledTextMessage CreateScheduledTextMessage()
    {
        return new ScheduledTextMessage
        {
            Data = new ScheduledTextMessageEvent(CustomerIds, Message).ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{Date} {Time}", CultureInfo.CurrentCulture)
        };
    }
}

public sealed class ScheduleTextMessageRequestValidator : AbstractValidator<ScheduleTextMessageRequest>
{
    public ScheduleTextMessageRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.TEXT_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.TEXT_MESSAGE]);

        RuleFor(p => p.Date)
             .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
             .WithMessage(localizer[ValidationErrorMessage.DATE]);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorMessage.TIME]);
    }
}
