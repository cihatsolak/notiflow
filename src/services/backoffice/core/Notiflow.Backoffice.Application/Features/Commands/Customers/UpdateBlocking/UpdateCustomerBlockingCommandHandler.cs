namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed class UpdateCustomerBlockingCommandHandler : IRequestHandler<UpdateCustomerBlockingCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;
    private readonly ILogger<UpdateCustomerBlockingCommandHandler> _logger;

    public UpdateCustomerBlockingCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ValidationErrorCodes> localizer,
        ILogger<UpdateCustomerBlockingCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.CUSTOMER_NOT_FOUND]);
        }

        if (customer.IsBlocked == request.IsBlocked)
        {
            _logger.LogWarning("The current disability situation is no different from the situation to be changed. Customer ID: {id}", request.Id);
            return Result<Unit>.Failure(StatusCodes.Status400BadRequest, _localizer[ValidationErrorCodes.CUSTOMER_BLOCKING_STATUS_EXISTS]);
        }

        customer.IsBlocked = request.IsBlocked;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Customer with id {id} is {status}.",
            request.Id,
            request.IsBlocked ? "blocked" : "unblocked");

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ValidationErrorCodes.CUSTOMER_BLOCK_STATUS_UPDATED], Unit.Value);
    }
}