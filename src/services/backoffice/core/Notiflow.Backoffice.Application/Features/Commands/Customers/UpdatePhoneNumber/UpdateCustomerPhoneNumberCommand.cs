namespace Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;

public sealed record UpdateCustomerPhoneNumberCommand : IRequest<Response<Unit>>
{
    public required int Id { get; init; }
    public required string PhoneNumber { get; init; }
}
