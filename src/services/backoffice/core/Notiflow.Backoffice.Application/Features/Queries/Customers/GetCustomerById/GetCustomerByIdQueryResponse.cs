namespace Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;

public sealed record GetCustomerByIdQueryResponse
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public DateTime BirthDate { get; init; }
    public Gender Gender { get; init; }
    public MarriageStatus MarriageStatus { get; init; }
    public bool IsBlocked { get; init; }
}
