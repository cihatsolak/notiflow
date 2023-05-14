namespace Notiflow.IdentityServer.Service.Tokens;

public interface ITokenService
{
    Response<TokenResponse> CreateToken(User user);
}