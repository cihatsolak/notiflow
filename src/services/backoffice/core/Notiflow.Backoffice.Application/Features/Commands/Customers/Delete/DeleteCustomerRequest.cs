namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;

public sealed record DeleteCustomerRequest : IRequest<Response>
{
    public int Id { get; init; }
}
