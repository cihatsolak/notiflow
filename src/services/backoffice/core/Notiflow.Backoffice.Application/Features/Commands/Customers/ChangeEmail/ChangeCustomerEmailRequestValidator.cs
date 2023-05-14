namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeEmail;

public sealed class ChangeCustomerEmailRequestValidator : AbstractValidator<ChangeCustomerEmailRequest>
{
    public ChangeCustomerEmailRequestValidator()
    {
        RuleFor(p => p.Id).InclusiveBetween(1, int.MaxValue).WithMessage("-1");
        RuleFor(p => p.Email).Email("-1");
    }
}
