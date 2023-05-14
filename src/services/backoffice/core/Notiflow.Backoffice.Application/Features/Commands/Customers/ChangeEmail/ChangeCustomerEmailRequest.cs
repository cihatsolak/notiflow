namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeEmail;

public sealed record ChangeCustomerEmailRequest : IRequest<Response<EmptyResponse>>
{
    public int Id { get; init; }
    public string Email { get; init; }
}
