namespace Notiflow.IdentityServer.Service.Tokens;

public interface ITokenService
{
    TokenResponse CreateToken(User user);
}