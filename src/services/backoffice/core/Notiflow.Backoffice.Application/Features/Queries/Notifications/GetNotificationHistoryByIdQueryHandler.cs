namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed record GetNotificationHistoryByIdQuery(int Id) : IRequest<Result<GetNotificationHistoryByIdQueryResult>>;

public sealed class GetNotificationHistoryByIdQueryHandler : IRequestHandler<GetNotificationHistoryByIdQuery, Result<GetNotificationHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetNotificationHistoryByIdQueryHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<GetNotificationHistoryByIdQueryResult>> Handle(GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var notificationHistory = await _uow.NotificationHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (notificationHistory is null)
        {
            return Result<GetNotificationHistoryByIdQueryResult>.Status404NotFound(ResultCodes.NOTIFICATION_NOT_FOUND);
        }

        var notificationDto = ObjectMapper.Mapper.Map<GetNotificationHistoryByIdQueryResult>(notificationHistory);
        return Result<GetNotificationHistoryByIdQueryResult>.Status200OK(ResultCodes.GENERAL_SUCCESS, notificationDto);
    }
}

public sealed class GetNotificationHistoryByIdQueryValidator : AbstractValidator<GetNotificationHistoryByIdQuery>
{
    public GetNotificationHistoryByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}
public sealed record GetNotificationHistoryByIdQueryResult
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Message { get; init; }
    public Guid SenderIdentity { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}