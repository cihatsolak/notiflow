namespace Notiflow.IdentityServer.Service.Auth;

internal class AuthManager : IAuthService
{
    private readonly ApplicationDbContext _appDbContext;
    private readonly ITokenService _tokenService;
    private readonly IClaimService _claimService;
    private readonly ILocalizerService<ResultState> _localizer;
    private readonly ILogger<AuthManager> _logger;

    public AuthManager(
        ApplicationDbContext appDbContext,
        ITokenService tokenService,
        IClaimService claimService,
        ILocalizerService<ResultState> localizer,
        ILogger<AuthManager> logger)
    {
        _appDbContext = appDbContext;
        _tokenService = tokenService;
        _claimService = claimService;
        _localizer = localizer;
        _logger = logger;
    }

    public async Task<Result<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users
            .TagWith("Get tenant and user information by username and password.")
            .AsNoTracking()
            .Include(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Username == request.Username && p.Password == request.Password, cancellationToken);
        if (user is null)
        {
            return Result<TokenResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_FOUND]);
        }

        var token = _tokenService.CreateToken(user);
        if (token is null)
        {
            _logger.LogWarning("Failed to generate access token for {username} user.", request.Username);
            return Result<TokenResponse>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultState.ACCESS_TOKEN_NOT_PRODUCED]);
        }

        return Result<TokenResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.ACCESS_TOKEN_GENERATED], token);
    }

    public async Task<Result<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await _appDbContext.RefreshTokens
            .TagWith("Get refresh token, tenant and user information based on refresh token.")
            .Include(p => p.User)
            .ThenInclude(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Token == request.Token, cancellationToken);
        if (refreshToken is null)
        {
            return Result<TokenResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.REFRESH_TOKEN_NOT_FOUND]);
        }

        var token = _tokenService.CreateToken(refreshToken.User);
        if (token is null)
        {
            _logger.LogWarning("Failed to generate access token for {username} user.", refreshToken.User.Username);
            return Result<TokenResponse>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultState.ACCESS_TOKEN_NOT_PRODUCED]);
        }

        refreshToken.Token = token.RefreshToken;
        refreshToken.ExpirationDate = token.RefreshTokenExpiration;

        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Result<TokenResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.ACCESS_TOKEN_GENERATED], token);
    }

    public async Task<Result<EmptyResponse>> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var refreshToken = await _appDbContext.RefreshTokens
            .TagWith("Get refresh token by refresh token.")
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Token == token, cancellationToken);
        if (refreshToken is null)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.REFRESH_TOKEN_NOT_FOUND]);
        }

        int numberOfRowsDeleted = await _appDbContext.RefreshTokens.Where(p => p.Token == refreshToken.Token).ExecuteDeleteAsync(cancellationToken);
        if (numberOfRowsDeleted != 1)
        {
            return Result<EmptyResponse>.Failure(StatusCodes.Status500InternalServerError, _localizer[ResultState.REFRESH_TOKEN_COULD_NOT_BE_DELETED]);
        }

        return Result<EmptyResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS]);
    }

    public async Task<Result<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var user = await _appDbContext.Users.FindAsync(new object[] { _claimService.NameIdentifier }, cancellationToken);
        if (user is null)
        {
            _logger.LogInformation("No authorized user found.");
            return Result<UserResponse>.Failure(StatusCodes.Status404NotFound, _localizer[ResultState.USER_NOT_FOUND]);
        }

        return Result<UserResponse>.Success(StatusCodes.Status200OK, _localizer[ResultState.GENERAL_SUCCESS], user.Adapt<UserResponse>());
    }
}