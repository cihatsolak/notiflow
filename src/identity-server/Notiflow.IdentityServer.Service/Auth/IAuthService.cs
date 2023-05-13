using Notiflow.IdentityServer.Service.Models.Users;

namespace Notiflow.IdentityServer.Service.Auth;

public interface IAuthService
{
    Task<Response<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
    Task<Response<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<Response<EmptyResponse>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<Response<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken);
}