namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

public sealed class GetDeviceByIdQueryHandler : IRequestHandler<GetDeviceByIdQuery, Response<GetDeviceByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetDeviceByIdQueryHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<GetDeviceByIdQueryResponse>> Handle(GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var device = await _unitOfWork.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Response<GetDeviceByIdQueryResponse>.Fail(-1);
        }

        var deviceResponse = ObjectMapper.Mapper.Map<GetDeviceByIdQueryResponse>(device);

        return Response<GetDeviceByIdQueryResponse>.Success(deviceResponse);
    }
}
