using Notiflow.Backoffice.Application.Localize;

namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, ApiResponse<GetNotificationHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetNotificationHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<GetNotificationHistoryByIdQueryResult>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            return ApiResponse<GetNotificationHistoryByIdQueryResult>.Failure(ResponseCodes.Error.NOTIFICATION_NOT_FOUND);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResult>(notificationHistory);
        return ApiResponse<GetNotificationHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, notificationDto);
    }
}