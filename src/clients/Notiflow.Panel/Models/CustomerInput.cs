namespace Notiflow.Panel.Models;

public class CustomerInput
{
    public int Id { get; set; }

    [Display(Name = "input.name.resource")]
    [DataType(DataType.Text)]
    public string Name { get; init; }

    [Display(Name = "input.surname.resource")]
    [DataType(DataType.Text)]
    public string Surname { get; init; }

    [Display(Name = "input.phonenumber.resource")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; init; }

    [Display(Name = "input.email.resource")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; init; }

    [Display(Name = "input.birthdate.resource")]
    [DataType(DataType.DateTime)]
    public DateTime BirthDate { get; init; }

    [Display(Name = "input.gender.resource")]
    public Gender Gender { get; init; }

    [Display(Name = "input.marriage.status.resource")]
    public MarriageStatus MarriageStatus { get; init; }
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