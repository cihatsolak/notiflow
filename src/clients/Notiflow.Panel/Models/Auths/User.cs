namespace Notiflow.Panel.Models.Auths;

internal sealed record User
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}
