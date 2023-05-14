namespace Notiflow.Backoffice.Application.Features.Commands.Customers.ChangePhoneNumber;

public sealed record ChangeCustomerPhoneNumberRequest : IRequest<Response<EmptyResponse>>
{
    public int Id { get; init; }
    public string PhoneNumber { get; init; }
}
