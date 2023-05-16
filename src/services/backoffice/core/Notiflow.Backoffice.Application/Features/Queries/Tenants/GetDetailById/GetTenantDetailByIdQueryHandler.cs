﻿namespace Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;

public sealed class GetTenantDetailByIdQueryHandler : IRequestHandler<GetTenantDetailByIdQuery, Response<GetTenantDetailByIdQueryResponse>>
{
    private readonly INotiflowUnitOfWork _unitOfWork;

    public GetTenantDetailByIdQueryHandler(INotiflowUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<GetTenantDetailByIdQueryResponse>> Handle(GetTenantDetailByIdQuery request, CancellationToken cancellationToken)
    {
        //var tenant = await _unitOfWork.TenantRead.GetByIdAsync(request.Id, cancellationToken);
        //if (tenant is null)
        //{
        //    return ResponseModel<GetDetailByIdQueryResponse>.Fail(ErrorCodes.TENANT_NOT_FOUND);
        //}

        //return ResponseModel<GetDetailByIdQueryResponse>.Success(SuccessCodes.TENANT_FOUND, ObjectMapper.Mapper.Map<GetDetailByIdQueryResponse>(tenant));

        return null;
    }
}