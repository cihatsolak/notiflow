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
        await _notiflowUnitOfWork.DeviceWrite.InsertAsync(ObjectMapper.Mapper.Map<Device>(request), cancellationToken);
        await _notiflowUnitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseModel<Unit>.Success(-1);
    }
}
