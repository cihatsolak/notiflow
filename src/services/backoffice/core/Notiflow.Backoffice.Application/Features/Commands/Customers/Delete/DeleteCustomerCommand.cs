namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed record DeleteCustomerCommand : IRequest<ApiResponse<Unit>>
{
    public required int Id { get; init; }
}
