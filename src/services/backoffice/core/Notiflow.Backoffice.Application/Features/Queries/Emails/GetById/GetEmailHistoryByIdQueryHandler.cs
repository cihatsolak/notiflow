using Notiflow.Common.Localize;

namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, ApiResponse<GetEmailHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetEmailHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ApiResponse<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            return ApiResponse<GetEmailHistoryByIdQueryResult>.Failure(ResponseCodes.Error.EMAIL_HISTORY_NOT_FOUND);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return ApiResponse<GetEmailHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, emailHistoryDto);
    }
}
