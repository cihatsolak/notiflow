namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
public sealed class AuthController : MainController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    
    [HttpPost("create-access-token")]
    [ProducesResponseType(typeof(ResponseData<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccessToken(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return Ok(tokenResponse);
    }

    [HttpPost("create-refresh-token")]
    [ProducesResponseType(typeof(ResponseData<TokenResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(refreshToken, cancellationToken);
        return Ok(tokenResponse);
    }

    [HttpDelete("revoke-refresh-token/{refreshToken:length(44)}")]
    public async Task<IActionResult> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return Ok(token);
    }

    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAuthenticatedUser(CancellationToken cancellationToken)
    {
        var user = await _authService.GetAuthenticatedUserAsync(cancellationToken);
        return Ok(user);
    }
}