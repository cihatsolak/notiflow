namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeBlocking;

public sealed record ChangeCustomerBlockingRequest : IRequest<Response<EmptyResponse>>
{
    public int Id { get; init; }
    public bool IsBlocked { get; init; }
}
