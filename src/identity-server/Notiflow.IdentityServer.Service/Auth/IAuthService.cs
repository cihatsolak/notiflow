using Notiflow.IdentityServer.Core.Models;

namespace Notiflow.IdentityServer.Service.Auth
{
    public interface IAuthService
    {
        Task<ResponseModel<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
        Task<ResponseModel<TokenResponse>> CreateAccessTokenAsync(string refreshToken, CancellationToken cancellationToken);
    }
}
