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

    public async Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.AsNoTracking().SingleOrDefaultAsync(p => p.Username == request.Username, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No user found with username {@username}.", request.Username);
            return ResponseData<TokenResponse>.Fail(-1);
        }

        var tokenResponse = _tokenService.CreateToken(user);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", request.Username);
            return ResponseData<TokenResponse>.Fail(-1);
        }

        return tokenResponse;
    }

    public async Task<ResponseData<TokenResponse>> CreateAccessTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var userRefreshToken = await _appDbContext.UserRefreshTokens.Include(p => p.User).SingleOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
        if (userRefreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return ResponseData<TokenResponse>.Fail(-1);
        }

        var tokenResponse = _tokenService.CreateToken(userRefreshToken.User);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", userRefreshToken.User.Username);
            return ResponseData<TokenResponse>.Fail(-1);
        }

        userRefreshToken.Token = tokenResponse.Data.RefreshToken;
        userRefreshToken.ExpirationDate = tokenResponse.Data.RefreshTokenExpiration;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return tokenResponse;
    }

    public async Task<Response> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var userRefreshToken = await _appDbContext.UserRefreshTokens.AsNoTracking().SingleOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
        if (userRefreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return Response.Fail(-1);
        }

        int numberOfRowsDeleted = await _appDbContext.UserRefreshTokens.Where(p => p.Token == userRefreshToken.Token).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete refresh token.");
            return Response.Fail(-1);
        }

        return Response.Success(-1);
    }

    public async Task<ResponseData<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FindAsync(new object[] { _claimService.NameIdentifier }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No authorized user found.");
            return ResponseData<UserResponse>.Fail(-1);
        }

        return ResponseData<UserResponse>.Success(user.Adapt<UserResponse>());
    }
}