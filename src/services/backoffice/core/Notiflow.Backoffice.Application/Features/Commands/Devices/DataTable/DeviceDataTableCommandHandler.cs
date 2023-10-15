namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed class DeviceDataTableCommandHandler : IRequestHandler<DeviceDataTableCommand, ApiResponse<DtResult<DeviceDataTableResult>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public DeviceDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<DtResult<DeviceDataTableResult>>> Handle(DeviceDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Device> devices) = await _uow.DeviceRead.GetPageAsync(request.SortKey,
                                                                                     request.SearchKey,
                                                                                     request.PageIndex,
                                                                                     request.PageSize,
                                                                                     cancellationToken
                                                                                     );

        if (devices.IsNullOrNotAny())
        {
            return ApiResponse<DtResult<DeviceDataTableResult>>.Failure(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        DtResult<DeviceDataTableResult> deviceDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<DeviceDataTableResult>>(devices)
        };

        return ApiResponse<DtResult<DeviceDataTableResult>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, deviceDataTable);
    }
}