namespace Notiflow.Schedule.Models;

public sealed record ScheduleNotificationRequest
{
    public required List<int> CustomerIds { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
    public required string Date { get; init; }
    public required string Time { get; init; }

    internal ScheduledNotification CreateScheduledNotification()
    {
        return new ScheduledNotification
        {
            Data = new ScheduledNotificationEvent(CustomerIds, Title, Message, ImageUrl).ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{Date} {Time}", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal)
        };
    }
}

public sealed class ScheduleNotificationRequestValidator : AbstractValidator<ScheduleNotificationRequest>
{
    public ScheduleNotificationRequestValidator()
    {
        RuleForEach(p => p.CustomerIds).Id(FluentVld.Errors.CUSTOMER_ID);

        RuleFor(p => p.Title)
            .Ensure(FluentVld.Errors.NOTIFICATION_TITLE)
            .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_TITLE);

        RuleFor(p => p.Message)
           .Ensure(FluentVld.Errors.NOTIFICATION_MESSAGE)
           .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_MESSAGE);

        RuleFor(p => p.ImageUrl)
           .Url(FluentVld.Errors.NOTIFICATION_IMAGE_URL)
           .MaximumLength(300).WithMessage(FluentVld.Errors.NOTIFICATION_IMAGE_URL);

        RuleFor(p => p.Date)
           .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
           .WithMessage(FluentVld.Errors.DATE);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(FluentVld.Errors.TIME);
    }
}