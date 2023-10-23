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
    public ScheduleTextMessageRequestValidator()
    {
        RuleFor(p => p.Date).Must(p => DateTime.TryParse(p, CultureInfo.CurrentCulture, out DateTime _)).WithMessage(string.Empty);
    }
}
