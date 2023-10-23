namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;

public sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, Result<GetDeviceByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultState> _localizer;

    public GetDeviceByIdQueryHandler(
        INotiflowUnitOfWork uow,
        ILocalizerService<ResultState> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<GetDeviceByIdQueryResult>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _uow.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Result<GetDeviceByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.DEVICE_NOT_FOUND]);
        }

        var deviceDto = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResult>(device);

        return Result<GetDeviceByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], deviceDto);
    }
}
