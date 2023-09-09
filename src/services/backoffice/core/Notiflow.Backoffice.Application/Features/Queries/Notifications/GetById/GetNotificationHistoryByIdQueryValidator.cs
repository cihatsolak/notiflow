namespace Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;

public sealed class GetNotificationHistoryByIdQueryValidator : AbstractValidator<GetNotificationHistoryByIdQuery>
{
    public GetNotificationHistoryByIdQueryValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);
    }
}
