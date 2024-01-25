namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories;

public sealed record GetTextMessageHistoryByIdQuery(int Id) : IRequest<Result<GetTextMessageHistoryByIdQueryResult>>;

public sealed class GetTextMessageHistoryByIdQueryHandler : IRequestHandler<GetTextMessageHistoryByIdQuery, Result<GetTextMessageHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetTextMessageHistoryByIdQueryHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<GetTextMessageHistoryByIdQueryResult>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await _uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            return Result<GetTextMessageHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, ResultCodes.TEXT_MESSAGE_NOT_FOUND);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResult>(textMessageHistory);

        return Result<GetTextMessageHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, ResultCodes.GENERAL_SUCCESS, textMessageHistoryDto);
    }
}

public sealed class GetTextMessageHistoryByIdQueryValidator : AbstractValidator<GetTextMessageHistoryByIdQuery>
{
    public GetTextMessageHistoryByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record GetTextMessageHistoryByIdQueryResult
{
    public int Id { get; init; }
    public string Message { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}