namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed class GetDetailByIdQueryHandler : IRequestHandler<GetDetailByIdQueryRequest, ResponseModel<GetDetailByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetDetailByIdQueryHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseModel<GetDetailByIdQueryResponse>> Handle(GetDetailByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var tenant = await _unitOfWork.TenantRead.GetByIdAsync(request.Id, cancellationToken);
        if (tenant is null)
        {
            return ResponseModel<GetDetailByIdQueryResponse>.Fail(ErrorCodes.TENANT_NOT_FOUND);
        }

        return ResponseModel<GetDetailByIdQueryResponse>.Success(SuccessCodes.TENANT_FOUND, ObjectMapper.Mapper.Map<GetDetailByIdQueryResponse>(tenant));
    }
}