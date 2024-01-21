namespace Notiflow.Schedule.Models;

public sealed record ScheduleNotificationRequest
{
    public required List<int> CustomerIds { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
    public required string Date { get; init; }
    public required string Time { get; init; }

    internal string ToScheduledNotificationEvent()
    {
        return new
        {
            CustomerIds,
            Message,
            ImageUrl,
            Title
        }.ToJson();
    }

    internal DateTime PlannedDeliveryDate => DateTime.Parse($"{Date} {Time}", CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal);
}

public sealed class ScheduleNotificationRequestValidator : AbstractValidator<ScheduleNotificationRequest>
{
    public ScheduleNotificationRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleForEach(p => p.CustomerIds).Id(localizer[ValidationErrorMessage.CUSTOMER_ID]);

        RuleFor(p => p.Title)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.NOTIFICATION_TITLE])
            .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_TITLE]);

        RuleFor(p => p.Message)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.NOTIFICATION_MESSAGE])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_MESSAGE]);

        RuleFor(p => p.ImageUrl)
           .NotNullAndNotEmpty(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL])
           .MaximumLength(300).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL])
           .Must(BeAValidUrl).WithMessage(localizer[ValidationErrorMessage.NOTIFICATION_IMAGE_URL]);

        RuleFor(p => p.Date)
           .Must(date => DateTime.TryParse(date, CultureInfo.CurrentCulture, out _))
           .WithMessage(localizer[ValidationErrorMessage.DATE]);

        RuleFor(p => p.Time)
            .Must(date => TimeSpan.TryParse(date, CultureInfo.CurrentCulture, out _))
            .WithMessage(localizer[ValidationErrorMessage.TIME]);
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
}