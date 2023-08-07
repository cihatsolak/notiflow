namespace Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;

public sealed record CustomerDataTableResponse
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
    public string Email { get; init; }
    public bool IsBlocked { get; init; }
    public bool IsDeleted { get; init; }
    public CloudMessagePlatform CloudMessagePlatform { get; set; }
}
