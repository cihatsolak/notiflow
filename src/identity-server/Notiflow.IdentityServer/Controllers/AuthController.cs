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
    /// Creates a new access token for the given user credentials.
    /// </summary>
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
    /// Creates a new access token from a refresh token.
    /// </summary>
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
    /// Revokes the specified refresh token.
    /// </summary>
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
    /// Gets the authenticated user's information.
    /// </summary>
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