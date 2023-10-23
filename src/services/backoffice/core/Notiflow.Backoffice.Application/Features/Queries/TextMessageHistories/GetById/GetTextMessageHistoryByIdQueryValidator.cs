namespace Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;

public sealed class GetTextMessageHistoryByIdQueryValidator : AbstractValidator<GetTextMessageHistoryByIdQuery>
{
    public GetTextMessageHistoryByIdQueryValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);
    }
}
