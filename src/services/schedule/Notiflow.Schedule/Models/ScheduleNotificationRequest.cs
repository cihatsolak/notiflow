namespace Notiflow.Schedule.Models;

public sealed record ScheduleNotificationRequest
{
    public required List<int> CustomerIds { get; init; }
    public required string Title { get; init; }
    public required string Message { get; init; }
    public required string ImageUrl { get; init; }
    public required string Date { get; init; }
    public required string Time { get; init; }
}

public sealed class ScheduleNotificationRequestValidator : AbstractValidator<ScheduleNotificationRequest>
{
    public ScheduleNotificationRequestValidator()
    {
        
    }
}