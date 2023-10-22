namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed class UpdateCustomerEmailCommandHandler : IRequestHandler<UpdateCustomerEmailCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly ILogger<UpdateCustomerEmailCommandHandler> _logger;

    public UpdateCustomerEmailCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ValidationErrorCodes> localizer, 
        ILogger<UpdateCustomerEmailCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMER_NOT_FOUND]);
        }

        if (string.Equals(customer.Email, request.Email, StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogWarning("The e-mail address to be changed is the same as in the current one. Customer ID: {id}", request.Id);
            return Result<Unit>.Failure(StatusCodes.Status400BadRequest, _localizer[ValidationErrorCodes.CUSTOMER_EMAIL_ADDRESS_SAME]);
        }

        customer.Email = request.Email;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The customer's email address has been updated. ID: {id}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ValidationErrorCodes.CUSTOMER_EMAIL_UPDATED], Unit.Value);
    }
}
