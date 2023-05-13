﻿namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteCustomerRequestHandler> _logger;

    public DeleteCustomerRequestHandler(
        INotiflowUnitOfWork uow,
        ILogger<DeleteCustomerRequestHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<EmptyResponse>> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _uow.CustomerWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete user of ID {@userId}.", request.Id);
            return Response<EmptyResponse>.Fail(-1);
        }

        return Response<EmptyResponse>.Success(1);
    }
}