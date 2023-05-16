namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandHandler : IRequestHandler<UpdateDeviceTokenCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceTokenCommandHandler> _logger;

    public UpdateDeviceTokenCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateDeviceTokenCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(1, cancellationToken);
        if (device is null)
        {
            _logger.LogWarning("The device with id {@deviceId} was not found.", request.Id);
            return Response<Unit>.Fail(-1);
        }

        device.Token = request.Token;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The token information of the device with {@deviceId} ids has been updated.", request.Id);

        return Response<Unit>.Success(1);
    }
}
