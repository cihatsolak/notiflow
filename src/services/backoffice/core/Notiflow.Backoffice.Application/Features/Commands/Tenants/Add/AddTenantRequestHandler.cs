namespace Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;

public sealed class AddTenantRequestHandler : IRequestHandler<AddTenantRequest, ResponseData<int>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public AddTenantRequestHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseData<int>> Handle(AddTenantRequest request, CancellationToken cancellationToken)
    {
        //var tenant = ObjectMapper.Mapper.Map<Tenant>(request);

        //await _unitOfWork.TenantWrite.InsertAsync(tenant, cancellationToken);
        //await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ResponseData<int>.Success(-1);
    }
}
