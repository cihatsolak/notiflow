namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator(ILocalizerService<ResultState> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ResultState.ID_NUMBER]);

        RuleFor(p => p.Name)
           .NotNullAndNotEmpty(localizer[ResultState.CUSTOMER_NAME])
           .MaximumLength(50).WithMessage(localizer[ResultState.CUSTOMER_NAME]);

        RuleFor(p => p.Surname)
            .NotNullAndNotEmpty(localizer[ResultState.CUSTOMER_SURNAME])
            .MaximumLength(75).WithMessage(localizer[ResultState.CUSTOMER_SURNAME]);

        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ResultState.PHONE_NUMBER]);

        RuleFor(p => p.Email).Email(localizer[ResultState.EMAIL]);

        RuleFor(p => p.BirthDate).BirthDate(localizer[ResultState.BIRTH_DATE]);

        RuleFor(p => p.Gender).Enum(localizer[ResultState.GENDER]);

        RuleFor(p => p.MarriageStatus).Enum(localizer[ResultState.MARRIAGE_STATUS]);
    }
}