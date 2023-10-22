namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed class GetTextMessageHistoryByIdQueryValidator : AbstractValidator<GetTextMessageHistoryByIdQuery>
{
    public GetTextMessageHistoryByIdQueryValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
