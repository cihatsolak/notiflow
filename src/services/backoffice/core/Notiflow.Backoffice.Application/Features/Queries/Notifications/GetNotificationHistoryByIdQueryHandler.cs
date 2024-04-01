namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery(int Id) : IRequest<Result<NotificationHistoryResponse>>;

public sealed class GetNotificationHistoryByIdQueryHandler(INotiflowUnitOfWork uow) 
    : IRequestHandler<GetNotificationHistoryByIdQuery, Result<NotificationHistoryResponse>>
{
    public async Task<Result<NotificationHistoryResponse>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            return Result<NotificationHistoryResponse>.Status404NotFound(ResultCodes.NOTIFICATION_NOT_FOUND);
        }

        return Result<NotificationHistoryResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, ObjectMapper.Mapper.Map<NotificationHistoryResponse>(notificationHistory));
    }
}

public sealed class GetNotificationHistoryByIdQueryValidator : AbstractValidator<GetNotificationHistoryByIdQuery>
{
    public GetNotificationHistoryByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record NotificationHistoryResponse
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Message { get; init; }
    public Guid SenderIdentity { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}