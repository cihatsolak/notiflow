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
    public ScheduleEmailRequestValidator()
    {
        
    }
}
