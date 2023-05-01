namespace Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;

public sealed class GetDeviceByIdHandler : IRequestHandler<GetDeviceByIdRequest, ResponseData<GetDeviceByIdResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetDeviceByIdHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseData<GetDeviceByIdResponse>> Handle(GetDeviceByIdRequest request, CancellationToken cancellationToken)
    {
        var device = await _unitOfWork.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return ResponseData<GetDeviceByIdResponse>.Fail(-1);
        }

        var deviceResponse = ObjectMapper.Mapper.Map<GetDeviceByIdResponse>(device);

        return ResponseData<GetDeviceByIdResponse>.Success(deviceResponse);
    }
}
