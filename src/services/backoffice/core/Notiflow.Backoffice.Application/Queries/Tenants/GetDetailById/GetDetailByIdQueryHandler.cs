namespace Notiflow.Backoffice.Application.Queries.Tenants.GetDetailById;

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
            return ResponseModel<GetDetailByIdQueryResponse>.Fail(1);
        }

        return ResponseModel<GetDetailByIdQueryResponse>.Success(ObjectMapper.Mapper.Map<GetDetailByIdQueryResponse>(tenant));
    }
}