namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, ApiResponse<int>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly ILogger<AddDeviceCommandHandler> _logger;

    public AddDeviceCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork, 
        ILogger<AddDeviceCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _logger = logger;
    }

    public async Task<ApiResponse<int>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is not null)
        {
            return ApiResponse<int>.Fail(ResponseCodes.Error.DEVICE_EXISTS);
        }

        device = ObjectMapper.Mapper.Map<Device>(request);
        await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new device with ID {@deviceId} has been added for the customer with ID number {@customerId}.", device.Id, device.CustomerId);

        return ApiResponse<int>.Success(ResponseCodes.Success.DEVICE_ASSOCIATED_CUSTOMER_ADDED, device.Id);
    }
}
