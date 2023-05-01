namespace Notiflow.IdentityServer.Service.Tokens;

public interface ITokenService
{
    ResponseData<TokenResponse> CreateToken(User user);
}