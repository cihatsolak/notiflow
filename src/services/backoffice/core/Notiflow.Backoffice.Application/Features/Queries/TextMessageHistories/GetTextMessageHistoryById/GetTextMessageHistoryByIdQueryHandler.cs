namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetTextMessageHistoryById;

public sealed class GetTextMessageHistoryByIdQueryHandler : IRequestHandler<GetTextMessageHistoryByIdQuery, Response<GetTextMessageHistoryByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetTextMessageHistoryByIdQueryHandler> _logger;

    public GetTextMessageHistoryByIdQueryHandler(INotiflowUnitOfWork uow, ILogger<GetTextMessageHistoryByIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<GetTextMessageHistoryByIdQueryResponse>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await _uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            _logger.LogWarning("{@textMessageHistoryId} ID message history not found.", request.Id);
            return Response<GetTextMessageHistoryByIdQueryResponse>.Fail(1);
        }

        var textMessageHistoryResponse = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResponse>(textMessageHistory);

        return Response<GetTextMessageHistoryByIdQueryResponse>.Success(1, textMessageHistoryResponse);
    }
}
