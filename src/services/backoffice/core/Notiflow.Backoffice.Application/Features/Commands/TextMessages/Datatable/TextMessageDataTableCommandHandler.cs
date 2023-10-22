namespace Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;

public sealed class TextMessageDataTableCommandHandler : IRequestHandler<TextMessageDataTableCommand, Result<DtResult<TextMessageDataTableCommandResult>>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;

    public TextMessageDataTableCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ValidationErrorCodes> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<DtResult<TextMessageDataTableCommandResult>>> Handle(TextMessageDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<TextMessageHistory> textMessageHistories) = await _uow.TextMessageHistoryRead.GetPageAsync(request.SortKey,
                                                                                                                           request.SearchKey,
                                                                                                                           request.PageIndex,
                                                                                                                           request.PageSize,
                                                                                                                           cancellationToken
                                                                                                                           );

        if (textMessageHistories.IsNullOrNotAny())
        {
            return Result<DtResult<TextMessageDataTableCommandResult>>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.TEXT_MESSAGE_NOT_FOUND]);
        }

        DtResult<TextMessageDataTableCommandResult> textMessageHistoryDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<TextMessageDataTableCommandResult>>(textMessageHistories)
        };

        return Result<DtResult<TextMessageDataTableCommandResult>>.Success(StatusCodes.Status200OK, _localizer[ValidationErrorCodes.GENERAL_SUCCESS], textMessageHistoryDataTable);
    }
}
