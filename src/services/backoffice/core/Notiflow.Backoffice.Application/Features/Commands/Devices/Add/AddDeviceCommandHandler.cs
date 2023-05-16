namespace Notiflow.Backoffice.Application.Features.Commands.Devices.Add;

public sealed class AddDeviceCommandHandler : IRequestHandler<AddDeviceCommand, Response<int>>
{
    private readonly INotiflowUnitOfWork _notiflowUnitOfWork;

    public AddDeviceCommandHandler(INotiflowUnitOfWork notiflowUnitOfWork)
    {
        _notiflowUnitOfWork = notiflowUnitOfWork;
    }

    public async Task<Response<int>> Handle(AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var device = await _notiflowUnitOfWork.DeviceRead.GetByIdAsync(1, cancellationToken);

        device.OSVersion = OSVersion.Other;
        

        //var device = ObjectMapper.Mapper.Map<Device>(request);
        //await _notiflowUnitOfWork.DeviceWrite.InsertAsync(device, cancellationToken);
        //await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        //device.OSVersion = OSVersion.Other;

        //_notiflowUnitOfWork.DeviceWrite.Update(device);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(-1);
    }
}
