namespace Notiflow.IdentityServer.Service.Auth;

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

    public async Task<ApiResponse<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users
            .AsNoTracking()
            .Include(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Username == request.Username, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No user found with username {@username}.", request.Username);
            return ApiResponse<TokenResponse>.Failure(-1);
        }

        var tokenResponse = _tokenService.CreateToken(user);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", request.Username);
            return ApiResponse<TokenResponse>.Failure(-1);
        }

        return tokenResponse;
    }

    public async Task<ApiResponse<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await _appDbContext.RefreshTokens
            .Include(p => p.User)
            .ThenInclude(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Token == request.Token, cancellationToken);
        if (refreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return ApiResponse<TokenResponse>.Failure(-1);
        }

        var tokenResponse = _tokenService.CreateToken(refreshToken.User);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", refreshToken.User.Username);
            return ApiResponse<TokenResponse>.Failure(-1);
        }

        refreshToken.Token = tokenResponse.Data.RefreshToken;
        refreshToken.ExpirationDate = tokenResponse.Data.RefreshTokenExpiration;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return tokenResponse;
    }

    public async Task<ApiResponse<EmptyResponse>> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var refreshToken = await _appDbContext.RefreshTokens.AsNoTracking().SingleOrDefaultAsync(p => p.Token == token, cancellationToken);
        if (refreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return ApiResponse<EmptyResponse>.Failure(-1);
        }

        int numberOfRowsDeleted = await _appDbContext.RefreshTokens.Where(p => p.Token == refreshToken.Token).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete refresh token.");
            return ApiResponse<EmptyResponse>.Failure(-1);
        }

        return ApiResponse<EmptyResponse>.Success(-1);
    }

    public async Task<ApiResponse<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FindAsync(new object[] { _claimService.NameIdentifier }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No authorized user found.");
            return ApiResponse<UserResponse>.Failure(-1);
        }

        return ApiResponse<UserResponse>.Success(user.Adapt<UserResponse>());
    }
}