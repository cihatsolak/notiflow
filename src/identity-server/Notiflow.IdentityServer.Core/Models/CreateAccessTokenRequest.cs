namespace Notiflow.IdentityServer.Core.Models;

public class CreateAccessTokenRequest
{
    public string EmailAddress { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}