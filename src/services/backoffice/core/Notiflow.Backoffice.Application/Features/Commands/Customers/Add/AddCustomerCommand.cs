namespace Notiflow.Backoffice.Application.Features.Commands.Customers.Add;

public sealed record AddCustomerCommand : IRequest<Result<int>>
{
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Email { get; init; }
    public required DateTime BirthDate { get; init; }
    public required Gender Gender { get; init; }
    public required MarriageStatus MarriageStatus { get; init; }
}
