using Notiflow.IdentityServer.Service.Models.Users;

namespace Notiflow.IdentityServer.Service.Auth;

public interface IAuthService
{
    Task<ApiResponse<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<ApiResponse<EmptyResponse>> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<ApiResponse<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken);
}