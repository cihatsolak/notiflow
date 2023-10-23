namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandHandler : IRequestHandler<UpdateDeviceTokenCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;
    private readonly ILogger<UpdateDeviceTokenCommandHandler> _logger;

    public UpdateDeviceTokenCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultState> localizer, 
        ILogger<UpdateDeviceTokenCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.DEVICE_NOT_FOUND]);
        }

        device.Token = request.Token;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The token information of the device with {deviceId} ids has been updated.", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ResultState.DEVICE_TOKEN_UPDATED], Unit.Value);
    }
}
