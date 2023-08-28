namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, Response<GetDeviceByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetDeviceByIdQueryHandler> _logger;

    public GetDeviceByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILogger<GetDeviceByIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<Response<GetDeviceByIdQueryResponse>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            _logger.LogInformation("Device with ID {@deviceId} was not found.", request.Id);
            return Response<GetDeviceByIdQueryResponse>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var deviceDto = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResponse>(device);

        return Response<GetDeviceByIdQueryResponse>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, deviceDto);
    }
}
