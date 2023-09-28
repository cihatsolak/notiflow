namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed class UpdateCustomerBlockingCommandHandler : IRequestHandler<UpdateCustomerBlockingCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateCustomerBlockingCommandHandler> _logger;

    public UpdateCustomerBlockingCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateCustomerBlockingCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogWarning("Customer not found. ID: {@id}", request.Id);
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMER_NOT_FOUND);
        }

        if (customer.IsBlocked == request.IsBlocked)
        {
            _logger.LogWarning("The current disability situation is no different from the situation to be changed. Customer ID: {@id}", request.Id);
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMER_BLOCKING_STATUS_EXISTS);
        }

        customer.IsBlocked = request.IsBlocked;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Customer with id {@id} is {@status}.",
            request.Id,
            request.IsBlocked ? "blocked" : "unblocked");

        return ApiResponse<Unit>.Success(ResponseCodes.Success.CUSTOMER_BLOCK_STATUS_UPDATED, Unit.Value);
    }
}