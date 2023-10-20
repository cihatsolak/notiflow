namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator(ILocalizerService<FluentValidationErrorCodes> localizer)
    {
        string message = localizer[FluentValidationErrorCodes.CUSTOMER_NAME];

        RuleFor(p => p.Name)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.CUSTOMER_NAME)
            .MaximumLength(50).WithMessage(FluentValidationErrorCodes.CUSTOMER_NAME);
       
        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(FluentValidationErrorCodes.CUSTOMER_SURNAME)
            .MaximumLength(75).WithMessage(FluentValidationErrorCodes.CUSTOMER_SURNAME);

        RuleFor(p => p.PhoneNumber).MobilePhone(FluentValidationErrorCodes.PHONE_NUMBER);

        RuleFor(p => p.Email).Email(FluentValidationErrorCodes.EMAIL);

        RuleFor(p => p.BirthDate).BirthDate(FluentValidationErrorCodes.BIRTH_DATE);

        RuleFor(p => p.Gender).Enum(FluentValidationErrorCodes.GENDER);

        RuleFor(p => p.MarriageStatus).Enum(FluentValidationErrorCodes.MARRIAGE_STATUS);
    }
}