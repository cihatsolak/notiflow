namespace Notiflow.Backoffice.Application.Features.Commands.Customers;

public sealed record UpdateCustomerEmailCommand(int Id, string Email) : IRequest<Result<Unit>>;

public sealed class UpdateCustomerEmailCommandHandler : IRequestHandler<UpdateCustomerEmailCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerEmailCommandHandler> _logger;

    public UpdateCustomerEmailCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<UpdateCustomerEmailCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Status404NotFound(ResultCodes.CUSTOMER_NOT_FOUND);
        }

        if (string.Equals(customer.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {customerId}", request.Id);
            return Result<Unit>.Status400BadRequest(ResultCodes.CUSTOMER_EMAIL_ADDRESS_SAME);
        }

        customer.Email = request.Email;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's email address has been updated. ID: {customerId}", request.Id);

        return Result<Unit>.Status204NoContent(ResultCodes.CUSTOMER_EMAIL_UPDATED);
    }
}

public sealed class ChangePhoneNumberRequestValidator : AbstractValidator<UpdateCustomerEmailCommand>
{
    public ChangePhoneNumberRequestValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
        RuleFor(p => p.Email).Email(localizer[ValidationErrorMessage.EMAIL]);
    }
}