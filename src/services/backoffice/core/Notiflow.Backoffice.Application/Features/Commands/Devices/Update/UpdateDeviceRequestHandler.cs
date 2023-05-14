namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Update;

public sealed class UpdateDeviceRequestHandler : IRequestHandler<UpdateDeviceRequest, Response<EmptyResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<UpdateDeviceRequestHandler> _logger;

    public UpdateDeviceRequestHandler(INotiflowUnitOfWork uow, ILogger<UpdateDeviceRequestHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<EmptyResponse>> Handle(UpdateDeviceRequest request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(1, cancellationToken);
        if (device is null)
        {
            _logger.LogWarning("The device with id {@id} was not found.", request.Id);
            return Response<EmptyResponse>.Fail(-1);
        }

        device.OSVersion = request.OSVersion;
        device.Code = request.Code;
        device.Token = request.Token;
        device.CloudMessagePlatform = request.CloudMessagePlatform;

        await _uow.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated device information with {@id} id.", request.Id);

        return Response<EmptyResponse>.Success(1);
    }
}
