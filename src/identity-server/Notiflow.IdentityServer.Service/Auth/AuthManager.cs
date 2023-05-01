using Mapster;

namespace Notiflow.IdentityServer.Service.Auth
{
    internal class AuthManager : IAuthService
    {
        private readonly ApplicationDbContext _appDbContext;
        private readonly ITokenService _tokenService;
        private readonly IClaimService _claimService;
        private readonly ILogger<AuthManager> _logger;

        public AuthManager(
            ApplicationDbContext appDbContext,
            ITokenService tokenService,
            IClaimService claimService,
            ILogger<AuthManager> logger)
        {
            _appDbContext = appDbContext;
            _tokenService = tokenService;
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<ResponseModel<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
        {
            User user;

            if (!string.IsNullOrWhiteSpace(request.Username))
            {
                user = await _appDbContext.Users.FirstOrDefaultAsync(p => p.Username == request.Username, cancellationToken);
            }
            else
            {
                user = await _appDbContext.Users.FirstOrDefaultAsync(p => p.Email == request.EmailAddress, cancellationToken);
            }


            if (user is null)
            {
                _logger.LogInformation("user not found.");
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            var tokenResponse = _tokenService.CreateTokenByUser(user);
            if (!tokenResponse.Succeeded)
            {
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            return tokenResponse;
        }

        public async Task<ResponseModel<TokenResponse>> CreateAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var userRefreshToken = await _appDbContext.UserRefreshTokens.Include(p => p.User).SingleOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
            if (userRefreshToken is null)
            {
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            var tokenResponse = _tokenService.CreateTokenByUser(userRefreshToken.User);
            if (!tokenResponse.Succeeded)
            {
                return ResponseModel<TokenResponse>.Fail(-1);
            }

            userRefreshToken.Token = tokenResponse.Data.RefreshToken;
            userRefreshToken.ExpirationDate = tokenResponse.Data.RefreshTokenExpiration;

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return tokenResponse;
        }

        public async Task<ResponseModel<int>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            var userRefreshToken = await _appDbContext.UserRefreshTokens.SingleOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
            if (userRefreshToken is null)
            {
                return ResponseModel<int>.Fail(-1);
            }

            await _appDbContext.UserRefreshTokens.Where(p => p.Token == userRefreshToken.Token).ExecuteDeleteAsync(cancellationToken);

            return ResponseModel<int>.Fail(-1);
        }

        public async Task<ResponseModel<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
        {
            var user = await _appDbContext.Users.FindAsync(new object[] { _claimService.NameIdentifier }, cancellationToken);
            if (user is null)
            {
                return ResponseModel<UserResponse>.Fail(-1);
            }

            return ResponseModel<UserResponse>.Success(user.Adapt<UserResponse>());
        }
    }
}
