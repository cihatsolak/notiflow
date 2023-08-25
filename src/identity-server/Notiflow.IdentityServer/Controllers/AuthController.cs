namespace Notiflow.IdentityServer.Controllers;

[AllowAnonymous]
public sealed class AuthController : BaseApiController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Creates an access token based on the provided request.
    /// </summary>
    /// <param name="request">The request containing information for access token creation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the access token result.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="400">invalid request</response>
    [HttpPost("create-access-token")]
    [ProducesResponseType(typeof(Response<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccessToken([FromBody] CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return HttpResult.Ok(response);
    }

    /// <summary>
    /// Creates an access token using a refresh token based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the refresh token for access token creation.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the access token result.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="400">invalid request</response>
    [HttpPost("create-refresh-token")]
    [ProducesResponseType(typeof(Response<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return HttpResult.Ok(response);
    }

    /// <summary>
    /// Revokes a refresh token based on the provided refresh token.
    /// </summary>
    /// <param name="refreshToken">The refresh token to revoke.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the refresh token revocation.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="400">invalid request</response>
    /// <response code="401">unauthorized user</response>
    [HttpDelete("revoke-refresh-token/{refreshToken:length(44)}")]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var response = await _authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Retrieves the authenticated user's details.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the authenticated user's details.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="404">user not found</response>
    [Authorize]
    [HttpGet("user")]
    [ProducesResponseType(typeof(Response<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthenticatedUser(CancellationToken cancellationToken)
    {
        var response = await _authService.GetAuthenticatedUserAsync(cancellationToken);
        return HttpResult.Get(response);
    }
}