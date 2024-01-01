namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed record GetTextMessageHistoryByIdQuery(int Id) : IRequest<Result<GetTextMessageHistoryByIdQueryResult>>;

public sealed class GetTextMessageHistoryByIdQueryHandler : IRequestHandler<GetTextMessageHistoryByIdQuery, Result<GetTextMessageHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;

    public GetTextMessageHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultMessage> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<GetTextMessageHistoryByIdQueryResult>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await _uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            return Result<GetTextMessageHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.TEXT_MESSAGE_NOT_FOUND]);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResult>(textMessageHistory);

        return Result<GetTextMessageHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultMessage.GENERAL_SUCCESS], textMessageHistoryDto);
    }
}
