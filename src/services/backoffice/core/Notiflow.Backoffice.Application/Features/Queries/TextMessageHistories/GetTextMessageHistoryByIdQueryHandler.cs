namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories;

public sealed record GetTextMessageHistoryByIdQuery(int Id) : IRequest<Result<GetTextMessageHistoryByIdQueryResult>>;

public sealed class GetTextMessageHistoryByIdQueryHandler(INotiflowUnitOfWork uow) : IRequestHandler<GetTextMessageHistoryByIdQuery, Result<GetTextMessageHistoryByIdQueryResult>>
{
    public async Task<Result<GetTextMessageHistoryByIdQueryResult>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            return Result<GetTextMessageHistoryByIdQueryResult>.Status404NotFound(ResultCodes.TEXT_MESSAGE_NOT_FOUND);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<GetTextMessageHistoryByIdQueryResult>(textMessageHistory);

        return Result<GetTextMessageHistoryByIdQueryResult>.Status200OK(ResultCodes.GENERAL_SUCCESS, textMessageHistoryDto);
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