namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, Response<GetEmailHistoryByIdQueryResult>>
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

    public async Task<Response<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            _logger.LogInformation("Email history with ID {@notificationId} was not found.", request.Id);
            return Response<GetEmailHistoryByIdQueryResult>.Fail(ResponseCodes.Error.EMAIL_HISTORY_NOT_FOUND);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return Response<GetEmailHistoryByIdQueryResult>.Success(ResponseCodes.Success.OPERATION_SUCCESSFUL, emailHistoryDto);
    }
}
