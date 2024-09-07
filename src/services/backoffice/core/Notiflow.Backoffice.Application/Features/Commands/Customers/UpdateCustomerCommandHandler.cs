namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerCommand(
    int Id,
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    DateTime BirthDate,
    Gender Gender,
    MarriageStatus MarriageStatus
    )
    : IRequest<Result>;

public sealed class UpdateCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateCustomerCommandHandler> logger) : IRequestHandler<UpdateCustomerCommand, Result>
{
    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        ObjectMapper.Mapper.Map(request, customer);

        uow.CustomerWrite.Update(customer);
        await uow.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Customer updated. ID: {customerId}", request.Id);

        return Result.Status204NoContent();
    }
}

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private const int CUSTOMER_NAME_MAX_LENGTH = 50;
    private const int CUSTOMER_SURNAME_MAX_LENGTH = 75;

    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.Id).Id(FluentVld.Errors.ID_NUMBER);
        RuleFor(p => p.Name).Ensure(FluentVld.Errors.CUSTOMER_NAME, CUSTOMER_NAME_MAX_LENGTH);
        RuleFor(p => p.Surname).Ensure(FluentVld.Errors.CUSTOMER_SURNAME, CUSTOMER_SURNAME_MAX_LENGTH);
        RuleFor(p => p.PhoneNumber).MobilePhone(FluentVld.Errors.PHONE_NUMBER);
        RuleFor(p => p.Email).Email(FluentVld.Errors.EMAIL);
        RuleFor(p => p.BirthDate).BirthDate(FluentVld.Errors.BIRTH_DATE);
        RuleFor(p => p.Gender).Enum(FluentVld.Errors.GENDER);
        RuleFor(p => p.MarriageStatus).Enum(FluentVld.Errors.MARRIAGE_STATUS);
    }
}