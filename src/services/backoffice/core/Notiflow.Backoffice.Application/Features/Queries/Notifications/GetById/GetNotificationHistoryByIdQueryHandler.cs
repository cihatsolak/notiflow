namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, Result<GetNotificationHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;

    public GetNotificationHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultState> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<GetNotificationHistoryByIdQueryResult>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            return Result<GetNotificationHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.NOTIFICATION_NOT_FOUND]);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResult>(notificationHistory);
        return Result<GetNotificationHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], notificationDto);
    }
}