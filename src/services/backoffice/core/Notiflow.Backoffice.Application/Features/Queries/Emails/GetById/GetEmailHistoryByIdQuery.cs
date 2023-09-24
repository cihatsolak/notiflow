namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed record GetEmailHistoryByIdQuery : IRequest<Response<GetEmailHistoryByIdQueryResult>>
{
    public required int Id { get; init; }
}
