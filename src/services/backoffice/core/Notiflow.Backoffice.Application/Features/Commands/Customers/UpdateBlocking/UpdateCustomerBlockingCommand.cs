namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed record UpdateCustomerBlockingCommand : IRequest<Result<Unit>>
{
    public required int Id { get; init; }
    public required bool IsBlocked { get; init; }
}
