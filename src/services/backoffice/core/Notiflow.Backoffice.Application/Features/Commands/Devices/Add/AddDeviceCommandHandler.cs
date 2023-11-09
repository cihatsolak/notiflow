namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, Result<int>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<AddDeviceCommandHandler> _logger;

    public AddDeviceCommandHandler(
        INotiflowUnitOfWork notiflowUnitOfWork,
        ILocalizerService<ResultMessage> localizer, 
        ILogger<AddDeviceCommandHandler> logger)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByCustomerIdAsync(request.CustomerId, cancellationToken);
        if (device is not null)
        {
            return Result<int>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.DEVICE_EXISTS]);
        }

        device = ObjectMapper.Mapper.Map<Device>(request);
        await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("A new device with ID {@deviceId} has been added for the customer with ID number {customerId}.", device.Id, device.CustomerId);

        return Result<int>.Success(StatusCodes.Status201Created, _localizer[ResultMessage.DEVICE_ASSOCIATED_CUSTOMER_ADDED], device.Id);
    }
}
