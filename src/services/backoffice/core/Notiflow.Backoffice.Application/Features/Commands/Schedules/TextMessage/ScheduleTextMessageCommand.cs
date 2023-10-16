namespace Notiflow.Backoffice.Application.Features.Commands.Schedules.TextMessageDelivery;

public sealed class ScheduleTextMessageCommand : IRequest<ApiResponse<EmptyResponse>>
{
    public required List<int> CustomerIds { get; init; }
    public required string Message { get; init; }
    public string Date  { get; set; }
    public string Time { get; set; }
}
