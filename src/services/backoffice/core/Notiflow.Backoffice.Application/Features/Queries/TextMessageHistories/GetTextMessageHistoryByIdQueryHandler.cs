namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories;

public sealed record GetTextMessageHistoryByIdQuery(int Id) : IRequest<Result<TextMessageHistoryResponse>>;

public sealed class GetTextMessageHistoryByIdQueryHandler(INotiflowUnitOfWork uow) : IRequestHandler<GetTextMessageHistoryByIdQuery, Result<TextMessageHistoryResponse>>
{
    public async Task<Result<TextMessageHistoryResponse>> Handle(GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var textMessageHistory = await uow.TextMessageHistoryRead.GetAsync(textMessageHistory => textMessageHistory.Id == request.Id, cancellationToken: cancellationToken);
        if (textMessageHistory is null)
        {
            return Result<TextMessageHistoryResponse>.Status404NotFound(ResultCodes.TEXT_MESSAGE_NOT_FOUND);
        }

        var textMessageHistoryDto = ObjectMapper.Mapper.Map<TextMessageHistoryResponse>(textMessageHistory);

        return Result<TextMessageHistoryResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, textMessageHistoryDto);
    }
}

public sealed class GetTextMessageHistoryByIdQueryValidator : AbstractValidator<GetTextMessageHistoryByIdQuery>
{
    public GetTextMessageHistoryByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record TextMessageHistoryResponse
{
    public int Id { get; init; }
    public string Message { get; init; }
    public bool IsSent { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}