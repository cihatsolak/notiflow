namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Response<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;

    public UpdateDeviceCommandHandler(INotiflowUnitOfWork uow, ILogger<UpdateDeviceCommandHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<Unit>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            _logger.LogWarning("The device with id {@id} was not found.", request.Id);
            return Response<Unit>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        ObjectMapper.Mapper.Map(request, device);

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated device information with {@id} id.", request.Id);

        return Response<Unit>.Success(ResponseCodes.Success.DEVICE_UPDATED, Unit.Value);
    }
}
