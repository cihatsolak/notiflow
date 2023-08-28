namespace Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;

public sealed record GetEmailHistoryByIdQuery : IRequest<Response<GetEmailHistoryByIdQueryResponse>>
{
    public required int Id { get; init; }
}
