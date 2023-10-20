using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed class TextMessageDataTableCommandHandler : IRequestHandler<TextMessageDataTableCommand, ApiResponse<DtResult<TextMessageDataTableCommandResult>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public TextMessageDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<DtResult<TextMessageDataTableCommandResult>>> Handle(TextMessageDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<TextMessageHistory> textMessageHistories) = await _uow.TextMessageHistoryRead.GetPageAsync(request.SortKey,
                                                                                                                           request.SearchKey,
                                                                                                                           request.PageIndex,
                                                                                                                           request.PageSize,
                                                                                                                           cancellationToken
                                                                                                                           );

        if (textMessageHistories.IsNullOrNotAny())
        {
            return ApiResponse<DtResult<TextMessageDataTableCommandResult>>.Failure(ResponseCodes.Error.TEXT_MESSAGE_NOT_FOUND);
        }

        DtResult<TextMessageDataTableCommandResult> textMessageHistoryDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<TextMessageDataTableCommandResult>>(textMessageHistories)
        };

        return ApiResponse<DtResult<TextMessageDataTableCommandResult>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, textMessageHistoryDataTable);
    }
}
