namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, ApiResponse<GetEmailHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILogger<GetEmailHistoryByIdQueryHandler> _logger;

    public GetEmailHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow,
        ILogger<GetEmailHistoryByIdQueryHandler> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    public async Task<ApiResponse<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            return ApiResponse<GetEmailHistoryByIdQueryResult>.Fail(ResponseCodes.Error.EMAIL_HISTORY_NOT_FOUND);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return ApiResponse<GetEmailHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, emailHistoryDto);
    }
}
