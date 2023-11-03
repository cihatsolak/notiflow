namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;

public sealed class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<DeleteDeviceCommandHandler> _logger;

    public DeleteDeviceCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultMessage> localizer, 
        ILogger<DeleteDeviceCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        bool isDeleted = await _uow.DeviceWrite.ExecuteDeleteByIdAsync(request.Id, cancellationToken);
        if (!isDeleted)
        {
            return Result<Unit>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultMessage.DEVICE_NOT_DELETED]);
        }

        _logger.LogInformation("The device with ID {deviceId} has been deleted.", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.DEVICE_DELETED], Unit.Value);
    }
}
