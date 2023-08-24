namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<Unit>>
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

    public async Task<Response<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _uow.CustomerWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete customer of ID {@customerId}.", request.Id);
            return Response<Unit>.Fail(ResponseCodes.Error.CUSTOMER_NOT_DELETED);
        }

        return Response<Unit>.Success(ResponseCodes.Success.CUSTOMER_DELETED, Unit.Value);
    }
}
