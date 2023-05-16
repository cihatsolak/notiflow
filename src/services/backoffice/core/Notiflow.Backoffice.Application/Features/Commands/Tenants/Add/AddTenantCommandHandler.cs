namespace Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;

public sealed class AddTenantCommandHandler : IRequestHandler<AddTenantCommand, Response<int>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public AddTenantCommandHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<int>> Handle(AddTenantCommand request, CancellationToken cancellationToken)
    {
        //var tenant = ObjectMapper.Mapper.Map<Tenant>(request);

        //await _unitOfWork.TenantWrite.InsertAsync(tenant, cancellationToken);
        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Response<int>.Success(-1);
    }
}
