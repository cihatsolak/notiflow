namespace Notiflow.Backoffice.Application.Features.Commands.Devices;

public sealed record DeviceDataTableCommand : DtParameters, IRequest<Result<DtResult<DeviceDataTableResult>>>;

public sealed class DeviceDataTableCommandHandler : IRequestHandler<DeviceDataTableCommand, Result<DtResult<DeviceDataTableResult>>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;

    public DeviceDataTableCommandHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ResultMessage> localizer)
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
            return Result<DtResult<DeviceDataTableResult>>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.DEVICE_NOT_FOUND]);
        }

        DtResult<DeviceDataTableResult> deviceDataTable = new()
        {
            RecordsFiltered = recordsTotal,
            RecordsTotal = recordsTotal,
            Draw = request.Draw,
            Data = ObjectMapper.Mapper.Map<List<DeviceDataTableResult>>(devices)
        };

        return Result<DtResult<DeviceDataTableResult>>.Success(StatusCodes.Status200OK, _localizer[ResultMessage.GENERAL_SUCCESS], deviceDataTable);
    }
}

public sealed record DeviceDataTableResult
{
    public required int Id { get; init; }
    public OSVersion OSVersion { get; init; }
    public string Code { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; init; }
    public DateTime CreatedDate { get; init; }
    public string FullName { get; init; }
}