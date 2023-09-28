namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, ApiResponse<GetDeviceByIdQueryResult>>
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

    public async Task<ApiResponse<GetDeviceByIdQueryResult>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            _logger.LogInformation("Device with ID {@deviceId} was not found.", request.Id);
            return ApiResponse<GetDeviceByIdQueryResult>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var deviceDto = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResult>(device);

        return ApiResponse<GetDeviceByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, deviceDto);
    }
}
