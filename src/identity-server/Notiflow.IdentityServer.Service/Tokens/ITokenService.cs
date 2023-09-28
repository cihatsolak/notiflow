namespace Notiflow.IdentityServer.Service.Tokens;

public interface ITokenService
{
    ApiResponse<TokenResponse> CreateToken(User user);
}