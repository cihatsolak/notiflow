namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerRequestValidator : AbstractValidator<AddCustomerRequest>
{
    public AddCustomerRequestValidator()
    {
        RuleFor(p => p.Name).NotNullAndNotEmpty("-1").MaximumLength(50).WithMessage("-1");
        RuleFor(p => p.Surname).NotNullAndNotEmpty("-1").MaximumLength(75).WithMessage("-1");
        RuleFor(p => p.PhoneNumber).MobilePhone("-1");
        RuleFor(p => p.Email).Email("-1");
        RuleFor(p => p.BirthDate).BirthDate("-1");
        RuleFor(p => p.Gender).IsInEnum().WithMessage("-1");
        RuleFor(p => p.MarriageStatus).IsInEnum().WithMessage("-1");
    }
}