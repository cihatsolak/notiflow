namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<UpdateCustomerEmailCommand>
{
    public ChangePhoneNumberRequestValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ResultState.EMAIL]);
    }
}
