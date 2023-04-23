namespace Notiflow.IdentityServer.Service.Tokens
{
    public interface ITokenService
    {
        ResponseModel<TokenResponse> CreateTokenByUser(User user);
    }
}