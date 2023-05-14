namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeBlocking;

public sealed record ChangeBlockingRequest : IRequest<Response<EmptyResponse>>
{
    public int Id { get; init; }
    public bool IsBlocked { get; init; }
}
