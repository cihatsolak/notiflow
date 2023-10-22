namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryValidator : AbstractValidator<GetNotificationHistoryByIdQuery>
{
    public GetNotificationHistoryByIdQueryValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
    }
}
