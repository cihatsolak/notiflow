namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceCommandHandler : IRequestHandler<UpdateDeviceCommand, Result<Unit>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;
    private readonly ILogger<UpdateDeviceCommandHandler> _logger;

    public UpdateDeviceCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultMessage> localizer, 
        ILogger<UpdateDeviceCommandHandler> logger)
    {
        _uow = uow;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<Unit>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.DEVICE_NOT_FOUND]);
        }
        
        ObjectMapper.Mapper.Map(request, device);

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Device information updated. Device ID: {id}", request.Id);

        return Result<Unit>.Success(StatusCodes.Status204NoContent, _localizer[ResultMessage.DEVICE_UPDATED], Unit.Value);
    }
}
