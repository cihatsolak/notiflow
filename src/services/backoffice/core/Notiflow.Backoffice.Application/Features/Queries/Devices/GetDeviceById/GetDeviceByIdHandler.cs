namespace Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

public sealed class GetDeviceByIdHandler : IRequestHandler<GetDeviceByIdRequest, Response<GetDeviceByIdResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetDeviceByIdHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<GetDeviceByIdResponse>> Handle(GetDeviceByIdRequest request, CancellationToken cancellationToken)
    {
        var device = await _unitOfWork.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return Response<GetDeviceByIdResponse>.Fail(-1);
        }

        var deviceResponse = ObjectMapper.Mapper.Map<GetDeviceByIdResponse>(device);

        return Response<GetDeviceByIdResponse>.Success(deviceResponse);
    }
}
