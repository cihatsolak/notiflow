namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;

public sealed record UpdateCustomerEmailCommand : IRequest<ApiResponse<Unit>>
{
    public required int Id { get; init; }
    public required string Email { get; init; }
}
