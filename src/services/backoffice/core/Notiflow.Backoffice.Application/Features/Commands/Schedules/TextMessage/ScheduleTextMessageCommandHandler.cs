namespace Notiflow.Backoffice.Application.Features.Commands.Schedules.TextMessage;

public sealed class ScheduleTextMessageCommandHandler : IRequestHandler<ScheduleTextMessageCommand, ApiResponse<EmptyResponse>>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public ScheduleTextMessageCommandHandler(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<ApiResponse<EmptyResponse>> Handle(ScheduleTextMessageCommand request, CancellationToken cancellationToken)
    {
        if (DateTime.TryParse($"{request.Date} {request.Time}", CultureInfo.CurrentCulture, out DateTime deliveryDate))
        {
            return ApiResponse<EmptyResponse>.Failure(1);
        }

        var textMessageSendingPlannedEvent = new TextMessageSendingPlannedEvent()
        {
            CustomerIds = request.CustomerIds,
            Message = request.Message,
            DeliveryDate = deliveryDate
        };

        await _publishEndpoint.Publish(textMessageSendingPlannedEvent, cancellationToken);

        return ApiResponse<EmptyResponse>.Success(1);
    }
}
