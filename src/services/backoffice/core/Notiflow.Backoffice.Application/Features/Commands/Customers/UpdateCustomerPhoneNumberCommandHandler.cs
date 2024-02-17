namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerPhoneNumberCommand(int Id, string PhoneNumber) : IRequest<Result<Unit>>;

public sealed class UpdateCustomerPhoneNumberCommandHandler : IRequestHandler<UpdateCustomerPhoneNumberCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerPhoneNumberCommandHandler> _logger;

    public UpdateCustomerPhoneNumberCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateCustomerPhoneNumberCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (string.Equals(customer.PhoneNumber, request.PhoneNumber))
        {
            _logger.LogWarning("The phone number to be changed is the same as in the current one. Customer ID: {customerId}", request.Id);
            return Result<Unit>.Status400BadRequest(ResultCodes.CUSTOMER_PHONE_NUMBER_SAME);
        }

        customer.PhoneNumber = request.PhoneNumber;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's phone number has been updated. ID: {customerId}", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.CUSTOMER_PHONE_NUMBER_UPDATED);
    }
}

public sealed class UpdateCustomerPhoneNumberCommandValidator : AbstractValidator<UpdateCustomerPhoneNumberCommand>
{
    public UpdateCustomerPhoneNumberCommandValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.PhoneNumber).MobilePhone(localizer[ValidationErrorMessage.PHONE_NUMBER]);
    }
}