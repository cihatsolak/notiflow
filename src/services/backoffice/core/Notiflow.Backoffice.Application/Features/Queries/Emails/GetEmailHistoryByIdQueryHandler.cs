﻿namespace Notiflow.Backoffice.Application.Features.Queries.Emails;

public sealed record GetEmailHistoryByIdQuery(int Id) : IRequest<Result<GetEmailHistoryByIdQueryResult>>;

public sealed class GetEmailHistoryByIdQueryHandler : IRequestHandler<GetEmailHistoryByIdQuery, Result<GetEmailHistoryByIdQueryResult>>
{
    private readonly INotiflowUnitOfWork _uow;

    public GetEmailHistoryByIdQueryHandler(INotiflowUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<Result<GetEmailHistoryByIdQueryResult>> Handle(GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var emailHistory = await _uow.EmailHistoryRead.GetByIdAsync(request.Id, cancellationToken);
        if (emailHistory is null)
        {
            return Result<GetEmailHistoryByIdQueryResult>.Failure(StatusCodes.Status404NotFound, ResultCodes.EMAIL_HISTORY_NOT_FOUND);
        }

        var emailHistoryDto = ObjectMapper.Mapper.Map<GetEmailHistoryByIdQueryResult>(emailHistory);
        return Result<GetEmailHistoryByIdQueryResult>.Success(StatusCodes.Status200OK, ResultCodes.GENERAL_SUCCESS, emailHistoryDto);
    }
}

public sealed class GetEmailHistoryByIdQueryValidator : AbstractValidator<GetEmailHistoryByIdQuery>
{
    public GetEmailHistoryByIdQueryValidator(ILocalizerService<ValidationErrorMessage> localizer)
    {
        RuleFor(p => p.Id).Id(localizer[ValidationErrorMessage.ID_NUMBER]);
    }
}

public sealed record GetEmailHistoryByIdQueryResult
{
    public int Id { get; init; }
    public string Recipients { get; init; }
    public string Cc { get; init; }
    public string Bcc { get; init; }
    public string Subject { get; init; }
    public string Body { get; init; }
    public bool IsSent { get; init; }
    public bool IsBodyHtml { get; init; }
    public string ErrorMessage { get; init; }
    public DateTime SentDate { get; init; }
}
