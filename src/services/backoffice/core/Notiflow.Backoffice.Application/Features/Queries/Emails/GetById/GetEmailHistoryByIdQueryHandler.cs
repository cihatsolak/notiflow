﻿namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed record GetEmailHistoryByIdQuery(int Id) : IRequest<Result<GetEmailHistoryByIdQueryResult>>;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, Result<GetEmailHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;
    private readonly ILocalizerService<ResultMessage> _localizer;

    public GetEmailHistoryByIdQueryHandler(
        INotiflowUnitOfWork uow, 
        ILocalizerService<ResultMessage> localizer)
    {
        _uow = uow;
        _localizer = localizer;
    }

    public async Task<Result<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            return Result<GetEmailHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, _localizer[ResultMessage.EMAIL_HISTORY_NOT_FOUND]);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return Result<GetEmailHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, _localizer[ResultMessage.GENERAL_SUCCESS], emailHistoryDto);
    }
}
