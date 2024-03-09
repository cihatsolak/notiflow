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

public sealed class AddCustomerCommandHandler : IRequestHandler<AddCustomerCommand, Result<int>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly IPublisher _publisher;
    private readonly ILogger<AddCustomerCommandHandler> _logger;

    public AddCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        IPublisher publisher,
        ILogger<AddCustomerCommandHandler> logger)
    {
        _uow = uow;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isExists = await _uow.CustomerRead.IsExistsByPhoneNumberOrEmailAsync(request.PhoneNumber, request.Email, cancellationToken);
        if (isExists)
        {
            return Result<int>.Status400BadRequest(ResultCodes.CUSTOMER_EXISTS);
        }

        var customer = ObjectMapper.Mapper.Map<Customer>(request);

        await _uow.CustomerWrite.InsertAsync(customer, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        await _publisher.Publish(new CustomerAddedNotification(customer.Id), cancellationToken);

        _logger.LogInformation("A new customer with {customerId} id has been registered.", customer.Id);

        return Result<int>.Status201Created(ResultCodes.CUSTOMER_ADDED, customer.Id);
    }
}

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