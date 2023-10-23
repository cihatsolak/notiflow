namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, Result<GetEmailHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;

    public GetEmailHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultState> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            return Result<GetEmailHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.EMAIL_HISTORY_NOT_FOUND]);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return Result<GetEmailHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], emailHistoryDto);
    }
}
