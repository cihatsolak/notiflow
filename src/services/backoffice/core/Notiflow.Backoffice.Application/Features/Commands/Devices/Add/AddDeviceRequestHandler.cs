namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceRequestHandler : IRequestHandler<AddDeviceRequest, ResponseModel<int>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;

    public AddDeviceRequestHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
    }

    public async Task<ResponseModel<int>> Handle(AddDeviceRequest request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByIdAsync(1, cancellationToken);

        device.OSVersion = OSVersion.Other;
        

        //var device = ObjectMapper.Mapper.Map<Device>(request);
        //await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        //await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        //device.OSVersion = OSVersion.Other;

        //_notiflowUnitOfWork.DeviceWrite.Update(device);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseModel<int>.Success(-1);
    }
}
