﻿namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, ApiResponse<GetNotificationHistoryByIdQueryResult>>
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

    public async Task<ApiResponse<GetNotificationHistoryByIdQueryResult>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            return ApiResponse<GetNotificationHistoryByIdQueryResult>.Fail(ResponseCodes.Error.NOTIFICATION_NOT_FOUND);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResult>(notificationHistory);
        return ApiResponse<GetNotificationHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, notificationDto);
    }
}