namespace Notiflow.Panel.Services.Authentication;

public interface IAuthService
{
    Task<bool> SignInAsync(SignInInput signInInput, CancellationToken cancellationToken);
    Task<Token> GetAccessTokenByRefreshTokenAsync(CancellationToken cancellationToken);
    Task RevokeRefreshTokenAsync(CancellationToken cancellationToken);
    Task SignOutAsync(CancellationToken cancellationToken);
}