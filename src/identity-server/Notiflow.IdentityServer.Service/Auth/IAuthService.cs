namespace Notiflow.IdentityServer.Service.Auth
{
    public interface IAuthService
    {
        Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken);
        Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Response> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<ResponseData<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken);
    }
}
