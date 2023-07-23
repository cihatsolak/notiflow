namespace Notiflow.IdentityServer.Service.Auth;

internal class AuthManager : IAuthService
{
    private readonly ApplicationDbContext _appDbContext;
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;
    private readonly IRedisService _redisService;
    private readonly ILogger<AuthManager> _logger;

    public AuthManager(
        ApplicationDbContext appDbContext,
        ITokenService tokenService,
        IClaimService claimService,
        IRedisService redisService,
        ILogger<AuthManager> logger)
    {
        _appDbContext = appDbContext;
        _tokenService = tokenService;
        _claimService = claimService;
        _redisService = redisService;
        _logger = logger;
    }

    public async Task<Response<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users
            .AsNoTracking()
            .Include(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Username == request.Username, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No user found with username {@username}.", request.Username);
            return Response<TokenResponse>.Fail(-1);
        }

        var tokenResponse = _tokenService.CreateToken(user);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", request.Username);
            return Response<TokenResponse>.Fail(-1);
        }

        await CheckCachedTenantInfoAsync(user);

        return tokenResponse;
    }

    public async Task<Response<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var userRefreshToken = await _appDbContext.UserRefreshTokens
            .Include(p => p.User)
            .ThenInclude(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Token == request.Token, cancellationToken);
        if (userRefreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return Response<TokenResponse>.Fail(-1);
        }

        var tokenResponse = _tokenService.CreateToken(userRefreshToken.User);
        if (!tokenResponse.Succeeded)
        {
            _logger.LogWarning("Failed to generate access token for {@username} user.", userRefreshToken.User.Username);
            return Response<TokenResponse>.Fail(-1);
        }

        userRefreshToken.Token = tokenResponse.Data.RefreshToken;
        userRefreshToken.ExpirationDate = tokenResponse.Data.RefreshTokenExpiration;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        await CheckCachedTenantInfoAsync(userRefreshToken.User);

        return tokenResponse;
    }

    public async Task<Response<EmptyResponse>> RevokeRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var userRefreshToken = await _appDbContext.UserRefreshTokens.AsNoTracking().SingleOrDefaultAsync(p => p.Token == refreshToken, cancellationToken);
        if (userRefreshToken is null)
        {
            _logger.LogInformation("Refresh token not found.");
            return Response<EmptyResponse>.Fail(-1);
        }

        int numberOfRowsDeleted = await _appDbContext.UserRefreshTokens.Where(p => p.Token == userRefreshToken.Token).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            _logger.LogInformation("Could not delete refresh token.");
            return Response<EmptyResponse>.Fail(-1);
        }

        return Response<EmptyResponse>.Success(-1);
    }

    public async Task<Response<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FindAsync(new object[] { _claimService.NameIdentifier }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No authorized user found.");
            return Response<UserResponse>.Fail(-1);
        }

        return Response<UserResponse>.Success(user.Adapt<UserResponse>());
    }

    private async Task CheckCachedTenantInfoAsync(User user)
    {
        
    }
}