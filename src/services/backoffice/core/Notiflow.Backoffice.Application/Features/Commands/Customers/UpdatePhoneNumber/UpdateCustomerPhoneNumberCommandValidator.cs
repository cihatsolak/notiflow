namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;

public sealed class UpdateCustomerPhoneNumberCommandValidator : AbstractValidator<UpdateCustomerPhoneNumberCommand>
{
    public UpdateCustomerPhoneNumberCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);
        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ResultState.PHONE_NUMBER]);
    }
}
