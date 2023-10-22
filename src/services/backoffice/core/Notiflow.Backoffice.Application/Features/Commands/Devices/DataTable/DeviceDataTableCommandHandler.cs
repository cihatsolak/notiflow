namespace Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;

public sealed class DeviceDataTableCommandHandler : IRequestHandler<DeviceDataTableCommand, Result<DtResult<DeviceDataTableResult>>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ValidationErrorCodes> _localizer;

    public DeviceDataTableCommandHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ValidationErrorCodes> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<DtResult<DeviceDataTableResult>>> Handle(DeviceDataTableCommand request, CancellationToken cancellationToken)
    {
        (int recordsTotal, List<Device> devices) = await _uow.DeviceRead.GetPageAsync(request.SortKey,
                                                                                     request.SearchKey,
                                                                                     request.PageIndex,
                                                                                     request.PageSize,
                                                                                     cancellationToken
                                                                                     );

        if (devices.IsNullOrNotAny())
        {
            return Result<DtResult<DeviceDataTableResult>>.Failure(StatusCodes.Status404NotFound, _localizer[ValidationErrorCodes.DEVICE_NOT_FOUND]);
        }

        DtResult<DeviceDataTableResult> deviceDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<DeviceDataTableResult>>(devices)
        };

        return Result<DtResult<DeviceDataTableResult>>.Success(StatusCodes.Status200OK, _localizer[ValidationErrorCodes.GENERAL_SUCCESS], deviceDataTable);
    }
}