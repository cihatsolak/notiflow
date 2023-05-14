namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeBlocking;

public sealed class ChangeCustomerBlockingRequestHandler : IRequestHandler<ChangeCustomerBlockingRequest, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<ChangeCustomerBlockingRequestHandler> _logger;

    public ChangeCustomerBlockingRequestHandler(INotiflowUnitOfWork uow, ILogger<ChangeCustomerBlockingRequestHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<EmptyResponse>> Handle(ChangeCustomerBlockingRequest request, CancellationToken cancellationToken)
    {
        var customer = await _uow.CustomerRead.GetByIdAsync(request.Id, cancellationToken);
        if (customer is null)
        {
            _logger.LogWarning("Customer not found. ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        if (customer.IsBlocked == request.IsBlocked)
        {
            _logger.LogWarning("The current disability situation is no different from the situation to be changed. Customer ID: {@id}", request.Id);
            return Response<EmptyResponse>.Fail(ErrorCodes.CUSTOMER_NOT_FOUND);
        }

        customer.IsBlocked = request.IsBlocked;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Customer with id {@id} is {@status}.",
            request.Id,
            request.IsBlocked ? "blocked" : "unblocked");

        return Response<EmptyResponse>.Success(-1);
    }
}