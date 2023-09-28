namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed class GetTextMessageHistoryByIdQueryHandler : IRequestHandler<GetTextMessageHistoryByIdQuery, ApiResponse<GetTextMessageHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetTextMessageHistoryByIdQueryHandler> _logger;

    public GetTextMessageHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILogger<GetTextMessageHistoryByIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<GetTextMessageHistoryByIdQueryResult>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await _uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            _logger.LogInformation("Text message history with ID {@textMessageHistoryId} was not found.", request.Id);
            return ApiResponse<GetTextMessageHistoryByIdQueryResult>.Fail(ResponseCodes.Error.TEXT_MESSAGE_NOT_FOUND);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResult>(textMessageHistory);

        return ApiResponse<GetTextMessageHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, textMessageHistoryDto);
    }
}
