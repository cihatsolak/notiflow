namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed record GetEmailHistoryByIdQuery : IRequest<Result<GetEmailHistoryByIdQueryResult>>
{
    public int Id { get; init; }

    public GetEmailHistoryByIdQuery(int id)
    {
        Id = id;
    }
}
