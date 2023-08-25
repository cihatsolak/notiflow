namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed class TextMessageDataTableCommandHandler : IRequestHandler<TextMessageDataTableCommand, Response<DtResult<TextMessageDataTableCommandResponse>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public TextMessageDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<DtResult<TextMessageDataTableCommandResponse>>> Handle(TextMessageDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<TextMessageHistory> textMessageHistories) = await _uow.TextMessageHistoryRead.GetPageAsync(request.SortKey,
                                                                                                                           request.SearchKey,
                                                                                                                           request.PageIndex,
                                                                                                                           request.PageSize,
                                                                                                                           cancellationToken
                                                                                                                           );

        if (textMessageHistories.IsNullOrNotAny())
        {
            return Response<DtResult<TextMessageDataTableCommandResponse>>.Fail(ResponseCodes.Error.TEXT_MESSAGE_NOT_FOUND);
        }

        DtResult<TextMessageDataTableCommandResponse> textMessageHistoryDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<TextMessageDataTableCommandResponse>>(textMessageHistories)
        };

        return Response<DtResult<TextMessageDataTableCommandResponse>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, textMessageHistoryDataTable);
    }
}
