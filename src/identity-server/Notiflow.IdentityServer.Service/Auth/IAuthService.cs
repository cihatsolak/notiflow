namespace Notiflow.IdentityServer.Service.Auth;

public interface IAuthService
{
    Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
    Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<Response> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
    Task<ResponseData<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken);
}