namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryValidator : AbstractValidator<GetEmailHistoryByIdQuery>
{
    public GetEmailHistoryByIdQueryValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
