﻿namespace Notiflow.IdentityServer.Service.Auth;

internal class AuthManager(
    ApplicationDbContext context,
    ITokenService tokenService,
    IClaimService claimService) : IAuthService
{
    public async Task<Result<TokenResponse>> CreateAccessTokenAsync(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .TagWith("Get tenant and user information by username and password.")
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Username == request.Username && p.Password == request.Password, cancellationToken);
        if (user is null)
        {
            return Result<TokenResponse>.Status404NotFound(ResultCodes.USER_NOT_FOUND);
        }

        var token = tokenService.CreateToken(user);
        if (token is null)
        {
            return Result<TokenResponse>.Status500InternalServerError(ResultCodes.ACCESS_TOKEN_NOT_PRODUCED);
        }

        var refreshToken = await context.RefreshTokens
            .TagWith("returns the user's refresh token.")
            .AsNoTracking()
            .SingleOrDefaultAsync(refreshToken => refreshToken.UserId == user.Id, cancellationToken);
        if (refreshToken is null)
        {
            refreshToken = new()
            {
                UserId = user.Id,
                Token = token.RefreshToken,
                ExpirationDate = token.RefreshTokenExpiration
            };

            await context.RefreshTokens.AddAsync(refreshToken, cancellationToken);
        }
        else
        {
            refreshToken.Token = token.RefreshToken;
            refreshToken.ExpirationDate = token.RefreshTokenExpiration;

            context.RefreshTokens.Update(refreshToken);
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result<TokenResponse>.Status200OK(ResultCodes.ACCESS_TOKEN_GENERATED, token);
    }

    public async Task<Result<TokenResponse>> CreateAccessTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .TagWith("Get refresh token, tenant and user information based on refresh token.")
            .Include(p => p.User)
            .ThenInclude(p => p.Tenant)
            .SingleOrDefaultAsync(p => p.Token == request.Token, cancellationToken);
        if (refreshToken is null)
        {
            return Result<TokenResponse>.Status404NotFound(ResultCodes.REFRESH_TOKEN_NOT_FOUND);
        }

        var token = tokenService.CreateToken(refreshToken.User);
        if (token is null)
        {
            return Result<TokenResponse>.Status500InternalServerError(ResultCodes.ACCESS_TOKEN_NOT_PRODUCED);
        }

        refreshToken.Token = token.RefreshToken;
        refreshToken.ExpirationDate = token.RefreshTokenExpiration;

        await context.SaveChangesAsync(cancellationToken);

        return Result<TokenResponse>.Status200OK(ResultCodes.ACCESS_TOKEN_GENERATED, token);
    }

    public async Task<Result> RevokeRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var refreshToken = await context.RefreshTokens
            .TagWith("Get refresh token by refresh token.")
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Token == token && p.User.Id == claimService.NameIdentifier, cancellationToken);
        if (refreshToken is null)
        {
            return Result.Status404NotFound(ResultCodes.REFRESH_TOKEN_NOT_FOUND);
        }

        int numberOfRowsDeleted = await context.RefreshTokens.Where(p => p.Token == refreshToken.Token).ExecuteDeleteAsync(cancellationToken);
        if (0 >= numberOfRowsDeleted)
        {
            return Result.Status500InternalServerError(ResultCodes.REFRESH_TOKEN_COULD_NOT_BE_DELETED);
        }

        return Result.Status200OK(ResultCodes.GENERAL_SUCCESS);
    }

    public async Task<Result<UserResponse>> GetAuthenticatedUserAsync(CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync([claimService.NameIdentifier], cancellationToken);
        if (user is null)
        {
            return Result<UserResponse>.Status404NotFound(ResultCodes.USER_NOT_FOUND);
        }

        return Result<UserResponse>.Status200OK(ResultCodes.GENERAL_SUCCESS, user.Adapt<UserResponse>());
    }
}