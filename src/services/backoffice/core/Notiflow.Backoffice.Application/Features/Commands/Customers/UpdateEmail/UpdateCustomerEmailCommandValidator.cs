namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<UpdateCustomerEmailCommand>
{
    public ChangePhoneNumberRequestValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ValidationErrorCodes.EMAIL]);
    }
}
