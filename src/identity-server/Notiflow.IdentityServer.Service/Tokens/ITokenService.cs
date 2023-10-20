namespace Notiflow.IdentityServer.Service.Tokens;

public interface ITokenService
{
    Result<TokenResponse> CreateToken(User user);
}