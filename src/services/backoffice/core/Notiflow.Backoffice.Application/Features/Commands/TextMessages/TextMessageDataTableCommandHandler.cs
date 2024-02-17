namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages;

public sealed record TextMessageDataTableCommand : DtParameters, IRequest<Result<DtResult<TextMessageDataTableCommandResult>>>;

public sealed class TextMessageDataTableCommandHandler(INotiflowUnitOfWork uow) : IRequestHandler<TextMessageDataTableCommand, Result<DtResult<TextMessageDataTableCommandResult>>>
{
    public async Task<Result<DtResult<TextMessageDataTableCommandResult>>> Handle(TextMessageDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<TextMessageHistory> textMessageHistories) = await uow.TextMessageHistoryRead.GetPageAsync(request.SortKey,
                                                                                                                           request.SearchKey,
                                                                                                                           request.PageIndex,
                                                                                                                           request.PageSize,
                                                                                                                           cancellationToken
                                                                                                                           );

        if (textMessageHistories.IsNullOrNotAny())
        {
            return Result<DtResult<TextMessageDataTableCommandResult>>.Status404NotFound(ResultCodes.TEXT_MESSAGE_NOT_FOUND);
        }

        DtResult<TextMessageDataTableCommandResult> textMessageHistoryDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<TextMessageDataTableCommandResult>>(textMessageHistories)
        };

        return Result<DtResult<TextMessageDataTableCommandResult>>.Status200OK(ResultCodes.GENERAL_SUCCESS, textMessageHistoryDataTable);
    }
}

public sealed record TextMessageDataTableCommandResult
{
    public required int Id { get; init; }
    public required string Message { get; init; }
    public required bool IsSent { get; init; }
    public required DateTime SentDate { get; init; }
}
