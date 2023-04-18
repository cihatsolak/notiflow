namespace Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;

public sealed class GetDeviceByIdHandler : IRequestHandler<GetDeviceByIdRequest, ResponseModel<GetDeviceByIdResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetDeviceByIdHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel<GetDeviceByIdResponse>> Handle(GetDeviceByIdRequest request, CancellationToken cancellationToken)
    {
        var device = await _unitOfWork.DeviceRead.GetByIdAsync(request.Id, cancellationToken);
        if (device is null)
        {
            return ResponseModel<GetDeviceByIdResponse>.Fail(-1);
        }

        var deviceResponse = ObjectMapper.Mapper.Map<GetDeviceByIdResponse>(device);

        return ResponseModel<GetDeviceByIdResponse>.Success(deviceResponse);
    }
}
