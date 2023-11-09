namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Name)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.CUSTOMER_NAME])
            .MaximumLength(50).WithMessage(localizer[ValidationErrorMessage.CUSTOMER_NAME]);
       
        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(localizer[ValidationErrorMessage.CUSTOMER_SURNAME])
            .MaximumLength(75).WithMessage(localizer[ValidationErrorMessage.CUSTOMER_SURNAME]);

        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorMessage.PHONE_NUMBER]);

        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);

        RuleFor(p => p.BirthDate).BirthDate(localizer[ValidationErrorMessage.BIRTH_DATE]);

        RuleFor(p => p.Gender).Enum(localizer[ValidationErrorMessage.GENDER]);

        RuleFor(p => p.MarriageStatus).Enum(localizer[ValidationErrorMessage.MARRIAGE_STATUS]);
    }
}