namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed class DeviceDataTableCommandHandler : IRequestHandler<DeviceDataTableCommand, Response<DtResult<DeviceDataTableResponse>>>
{
    private readonly INotiflowUnitOfWork _uow;

    public DeviceDataTableCommandHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Response<DtResult<DeviceDataTableResponse>>> Handle(DeviceDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Device> devices) = await _uow.DeviceRead.GetPageAsync(request.SortKey,
                                                                                     request.SearchKey,
                                                                                     request.PageIndex,
                                                                                     request.PageSize,
                                                                                     cancellationToken
                                                                                     );

        if (devices.IsNullOrNotAny())
        {
            return Response<DtResult<DeviceDataTableResponse>>.Fail(ResponseCodes.Error.DEVICE_NOT_FOUND);
        }

        DtResult<DeviceDataTableResponse> deviceDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<DeviceDataTableResponse>>(devices)
        };

        return Response<DtResult<DeviceDataTableResponse>>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, deviceDataTable);
    }
}