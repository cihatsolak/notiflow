namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed record DeleteCustomerCommand : IRequest<Result<Unit>>
{
    public required int Id { get; init; }
}
