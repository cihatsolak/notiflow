namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<DeleteDeviceCommandHandler> _logger;

    public DeleteDeviceCommandHandler(
        INotiflowUnitOfWork uow, 
        ILogger<DeleteDeviceCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.DeviceWrite.ExecuteDeleteAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return ApiResponse<Unit>.Failure(ResponseCodes.Error.DEVICE_NOT_DELETED);
        }

        _logger.LogInformation("The device with ID {deviceId} has been deleted.", request.Id);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.DEVICE_DELETED, Unit.Value);
    }
}
