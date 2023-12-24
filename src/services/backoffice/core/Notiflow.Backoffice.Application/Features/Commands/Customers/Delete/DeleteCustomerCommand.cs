namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed record DeleteCustomerCommand : IRequest<Result<Unit>>
{
    public int Id { get; init; }

    public DeleteCustomerCommand(int id)
    {
        Id = id;
    }
}
