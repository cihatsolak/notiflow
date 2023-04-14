namespace Notiflow.Backoffice.Application.Commands.Devices.Insert
{
    public sealed class InsertDeviceRequestHandler : IRequestHandler<InsertDeviceRequest, ResponseModel<Unit>>
    {
        private readonly INotiflowUnitOfWork _notiflowUnitOfWork;

        public InsertDeviceRequestHandler(INotiflowUnitOfWork notiflowUnitOfWork)
        {
            _notiflowUnitOfWork = notiflowUnitOfWork;
        }

        public Task<ResponseModel<Unit>> Handle(InsertDeviceRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
