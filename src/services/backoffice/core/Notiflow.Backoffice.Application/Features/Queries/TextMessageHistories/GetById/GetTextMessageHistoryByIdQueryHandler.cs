using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed class GetTextMessageHistoryByIdQueryHandler : IRequestHandler<GetTextMessageHistoryByIdQuery, ApiResponse<GetTextMessageHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetTextMessageHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<GetTextMessageHistoryByIdQueryResult>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await _uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            return ApiResponse<GetTextMessageHistoryByIdQueryResult>.Failure(ResponseCodes.Error.TEXT_MESSAGE_NOT_FOUND);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResult>(textMessageHistory);

        return ApiResponse<GetTextMessageHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, textMessageHistoryDto);
    }
}
