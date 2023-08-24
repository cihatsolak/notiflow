namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, Response<int>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly ILogger<AddDeviceCommandHandler> _logger;

    public AddDeviceCommandHandler(INotiflowUnitOfWork notiflowUnitOfWork, ILogger<AddDeviceCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _logger = logger;
    }

    public async Task<Response<int>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is not null)
        {
            _logger.LogInformation("There is a device that belongs to customer {@customerId}.", request.CustomerId);
            return Response<int>.Fail(ResponseCodes.Error.DEVICE_EXISTS);
        }

        device = ObjectMapper.Mapper.Map<Device>(request);
        await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new device has been added for the user with the ID number {@customerId}.", request.CustomerId);

        return Response<int>.Success(ResponseCodes.Success.DEVICE_ASSOCIATED_CUSTOMER_ADDED, device.Id);
    }
}
