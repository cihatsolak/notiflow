namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeEmail;

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<ChangeCustomerEmailRequest>
{
    public ChangePhoneNumberRequestValidator()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Email).Email("-1");
    }
}
