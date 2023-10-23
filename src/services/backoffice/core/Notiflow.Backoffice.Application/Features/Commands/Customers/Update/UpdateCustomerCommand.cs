namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

public sealed record UpdateCustomerCommand : IRequest<Result<Unit>>
{
    public required int Id { get; set; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required DateTime BirthDate { get; init; }
    public required Gender Gender { get; init; }
    public required MarriageStatus MarriageStatus { get; init; }
}
