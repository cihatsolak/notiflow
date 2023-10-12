namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, ApiResponse<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;

    public UpdateDeviceCommandHandler(
        INotiflowUnitOfWork uow, 
        ILogger<UpdateDeviceCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<Unit>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return ApiResponse<Unit>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }
        
        ObjectMapper.Mapper.Map(request, device);

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Device information updated. Device ID: {id}", request.Id);

        return ApiResponse<Unit>.Success(ResponseCodes.Success.DEVICE_UPDATED, Unit.Value);
    }
}
