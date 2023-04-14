namespace Notiflow.Backoffice.Application.Commands.Devices.Insert;

public sealed class InsertDeviceRequestHandler : IRequestHandler<InsertDeviceRequest, ResponseModel<Unit>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;

    public InsertDeviceRequestHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
    }

    public async Task<ResponseModel<Unit>> Handle(InsertDeviceRequest request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByIdAsync(1, cancellationToken);

        device.OSVersion = OSVersion.Windows;


        //var device = ObjectMapper.Mapper.Map<Device>(request);
        //await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        //await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        //device.OSVersion = OSVersion.Other;

        _notiflowUnitOfWork.DeviceWrite.Update(device);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseModel<Unit>.Success(-1);
    }
}
