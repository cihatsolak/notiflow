namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, ApiResponse<GetDeviceByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetDeviceByIdQueryHandler(
        INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<GetDeviceByIdQueryResult>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return ApiResponse<GetDeviceByIdQueryResult>.Failure(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        var deviceDto = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResult>(device);

        return ApiResponse<GetDeviceByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, deviceDto);
    }
}
