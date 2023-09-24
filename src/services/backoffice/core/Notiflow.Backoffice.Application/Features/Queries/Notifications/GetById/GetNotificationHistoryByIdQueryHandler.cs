namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, Response<GetNotificationHistoryByIdQueryResult>>
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

    public async Task<Response<GetNotificationHistoryByIdQueryResult>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            _logger.LogInformation("Notification with ID {@notificationId} was not found.", request.Id);
            return Response<GetNotificationHistoryByIdQueryResult>.Fail(ResponseCodes.Error.NOTIFICATION_NOT_FOUND);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResult>(notificationHistory);
        return Response<GetNotificationHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, notificationDto);
    }
}