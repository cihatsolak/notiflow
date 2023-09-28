namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;

public sealed record UpdateCustomerBlockingCommand : IRequest<ApiResponse<Unit>>
{
    public required int Id { get; init; }
    public required bool IsBlocked { get; init; }
}
