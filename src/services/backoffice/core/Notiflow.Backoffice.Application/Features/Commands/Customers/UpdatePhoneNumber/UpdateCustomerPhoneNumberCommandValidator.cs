namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;

public sealed class UpdateCustomerPhoneNumberCommandValidator : AbstractValidator<UpdateCustomerPhoneNumberCommand>
{
    public UpdateCustomerPhoneNumberCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentValidationErrorCodes.ID_NUMBER);
        RuleFor(p => p.PhoneNumber).MobilePhone(FluentValidationErrorCodes.PHONE_NUMBER);
    }
}
