namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, Response<GetNotificationHistoryByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetNotificationHistoryByIdQueryHandler> _logger;

    public GetNotificationHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow,
        ILogger<GetNotificationHistoryByIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<GetNotificationHistoryByIdQueryResponse>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            _logger.LogInformation("Notification with ID {@notificationId} was not found.", request.Id);
            return Response<GetNotificationHistoryByIdQueryResponse>.Fail(ResponseCodes.Error.NOTIFICATION_NOT_FOUND);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResponse>(notificationHistory);
        return Response<GetNotificationHistoryByIdQueryResponse>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, notificationDto);
    }
}
