namespace Notiflow.IdentityServer.Service.Auth;

public interface IAuthService
{
    Task<Result<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
    Task<Result<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
    Task<Result> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<Result<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken);
}