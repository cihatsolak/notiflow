namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<UpdateCustomerEmailCommand>
{
    public ChangePhoneNumberRequestValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);
        RuleFor(p => p.Email).Email(FluentValidationErrorCodes.EMAIL);
    }
}
