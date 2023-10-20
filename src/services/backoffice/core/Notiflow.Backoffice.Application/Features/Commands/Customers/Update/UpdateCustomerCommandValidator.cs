using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(ILocalizerService<ValidationErrorCodes> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorCodes.ID_NUMBER]);

        RuleFor(p => p.Name)
           .NotNullAndNotEmpty(localizer[ValidationErrorCodes.CUSTOMER_NAME])
           .MaximumLength(50).WithMessage(localizer[ValidationErrorCodes.CUSTOMER_NAME]);

        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(localizer[ValidationErrorCodes.CUSTOMER_SURNAME])
            .MaximumLength(75).WithMessage(localizer[ValidationErrorCodes.CUSTOMER_SURNAME]);

        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorCodes.PHONE_NUMBER]);

        RuleFor(p => p.Email).Email(localizer[ValidationErrorCodes.EMAIL]);

        RuleFor(p => p.BirthDate).BirthDate(localizer[ValidationErrorCodes.BIRTH_DATE]);

        RuleFor(p => p.Gender).Enum(localizer[ValidationErrorCodes.GENDER]);

        RuleFor(p => p.MarriageStatus).Enum(localizer[ValidationErrorCodes.MARRIAGE_STATUS]);
    }
}