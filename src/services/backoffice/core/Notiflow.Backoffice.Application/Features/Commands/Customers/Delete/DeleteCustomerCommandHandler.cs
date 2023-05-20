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
            _logger.LogInformation("Could not delete user of ID {@userId}.", request.Id);
            return Response<Unit>.Fail(-1);
        }

        return Response<Unit>.Success(1);
    }
}
