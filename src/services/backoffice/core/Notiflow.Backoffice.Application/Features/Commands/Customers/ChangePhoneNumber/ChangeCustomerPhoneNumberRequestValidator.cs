namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangePhoneNumber;

public sealed class ChangeCustomerPhoneNumberRequestValidator : AbstractValidator<ChangeCustomerPhoneNumberRequest>
{
    public ChangeCustomerPhoneNumberRequestValidator()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.PhoneNumber).MobilePhone("-1");
    }
}
