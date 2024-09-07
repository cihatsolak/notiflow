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
            PlannedDeliveryDate = DateTime.Parse($"{Date} {Time}", CultureInfo.InvariantCulture)
        };
    }
}

public sealed class ScheduleTextMessageRequestValidator : AbstractValidator<ScheduleTextMessageRequest>
{
    public ScheduleTextMessageRequestValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.Message)
           .Ensure(FluentVld.Errors.TEXT_MESSAGE)
           .MaximumLength(300).WithMessage(FluentVld.Errors.TEXT_MESSAGE);

        RuleFor(p => p.Date)
             .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
             .WithMessage(FluentVld.Errors.DATE);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(FluentVld.Errors.TIME);
    }
}
