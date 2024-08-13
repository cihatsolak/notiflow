namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record AddCustomerCommand(
    string Name,
    string Surname,
    string PhoneNumber,
    string Email,
    DateTime BirthDate,
    Gender Gender,
    MarriageStatus MarriageStatus
    )
    : IRequest<Result<int>>;

public sealed class AddCustomerCommandHandler(
    INotiflowUnitOfWork uow,
    IPublisher publisher,
    ILogger<AddCustomerCommandHandler> logger) : IRequestHandler<AddCustomerCommand, Result<int>>
{
    public async Task<Result<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            return Result<int>.Status400BadRequest(ResultCodes.CUSTOMER_EXISTS);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await uow.SaveChangesAsync(cancellationToken);

        await publisher.Publish(new CustomerAddedNotification(customer.Id), cancellationToken);

        logger.LogInformation("A new customer with {customerId} id has been registered.", customer.Id);

        return Result<int>.Status201Created(ResultCodes.CUSTOMER_ADDED, customer.Id);
    }
}

public sealed class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    private const int CUSTOMER_NAME_MAX_LENGTH = 50;
    private const int CUSTOMER_SURNAME_MAX_LENGTH = 75;

    public AddCustomerCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Name).Ensure(localizer[ValidationErrorMessage.CUSTOMER_NAME], CUSTOMER_NAME_MAX_LENGTH);
        RuleFor(p => p.Surname).Ensure(localizer[ValidationErrorMessage.CUSTOMER_SURNAME], CUSTOMER_SURNAME_MAX_LENGTH);
        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorMessage.PHONE_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);
        RuleFor(p => p.BirthDate).BirthDate(localizer[ValidationErrorMessage.BIRTH_DATE]);
        RuleFor(p => p.Gender).Enum(localizer[ValidationErrorMessage.GENDER]);
        RuleFor(p => p.MarriageStatus).Enum(localizer[ValidationErrorMessage.MARRIAGE_STATUS]);
    }
}