namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
public sealed class AuthController : MainController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Creates a new access token for the given user credentials.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpPost("create-access-token")]
    [ProducesResponseType(typeof(Response<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccessToken([FromBody] CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return Ok(tokenResponse);
    }

    /// <summary>
    /// Creates a new access token from a refresh token.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpPost("create-refresh-token")]
    [ProducesResponseType(typeof(Response<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return Ok(tokenResponse);
    }

    /// <summary>
    /// Revokes the specified refresh token.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpDelete("revoke-refresh-token/{refreshToken:length(44)}")]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return Ok(token);
    }

    /// <summary>
    /// Gets the authenticated user's information.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized user</response>
    /// <response code="404">User not found</response>
    [Authorize]
    [HttpGet("user")]
    [ProducesResponseType(typeof(Response<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAuthenticatedUser(CancellationToken cancellationToken)
    {
        var user = await _authService.GetAuthenticatedUserAsync(cancellationToken);
        return Ok(user);
    }
}