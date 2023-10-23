namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryValidator : AbstractValidator<GetEmailHistoryByIdQuery>
{
    public GetEmailHistoryByIdQueryValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);
    }
}
