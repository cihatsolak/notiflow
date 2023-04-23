namespace Notiflow.IdentityServer.Service.Auth
{
    public interface IAuthService
    {
        Task<ResponseModel<TokenResponse>> CreateTokenAsync(); 
    }
}
