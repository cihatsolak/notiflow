namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteCustomerCommandHandler> _logger;

    public DeleteCustomerCommandHandler(
        INotiflowUnitOfWork uow,
        ILogger<DeleteCustomerCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.CustomerWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.CUSTOMER_NOT_DELETED);
        }

        _logger.LogInformation("Customer deleted. ID: {customerId}", request.Id);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.CUSTOMER_DELETED, Unit.Value);
    }
}
