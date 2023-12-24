namespace Notiflow.Panel.Models;

public class CustomerInput
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

public enum Gender : byte
{
    Male = 1,
    Female
}

public enum MarriageStatus : byte
{
    Single = 1,
    Married
}