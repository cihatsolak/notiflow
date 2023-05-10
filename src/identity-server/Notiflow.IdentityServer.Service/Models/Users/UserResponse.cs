namespace Notiflow.IdentityServer.Service.Models.Users
{
    public sealed record UserResponse
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
