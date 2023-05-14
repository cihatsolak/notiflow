namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceRequestHandler : IRequestHandler<DeleteDeviceRequest, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteDeviceRequestHandler> _logger;

    public DeleteDeviceRequestHandler(INotiflowUnitOfWork uow, ILogger<DeleteDeviceRequestHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(DeleteDeviceRequest request, CancellationToken cancellationToken)
    {
        int numberOfRowsDeleted = await _uow.DeviceWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogWarning("Could not delete device of ID {@deviceId}.", request.Id);
            return Response<Unit>.Fail(-1);
        }

        _logger.LogInformation("The device with ID {@deviceId} has been deleted.", request.Id);

        return Response<Unit>.Success(1);
    }
}
