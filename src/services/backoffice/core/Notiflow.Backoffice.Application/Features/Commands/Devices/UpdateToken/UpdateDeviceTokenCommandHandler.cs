using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;

public sealed class UpdateDeviceTokenCommandHandler : IRequestHandler<UpdateDeviceTokenCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceTokenCommandHandler> _logger;

    public UpdateDeviceTokenCommandHandler(
        INotiflowUnitOfWork uow, 
        ILogger<UpdateDeviceTokenCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return ApiResponse<Unit>.Failure(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        device.Token = request.Token;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("The token information of the device with {deviceId} ids has been updated.", request.Id);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.DEVICE_TOKEN_UPDATED, Unit.Value);
    }
}
