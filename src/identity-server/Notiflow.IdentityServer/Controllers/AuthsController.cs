using Notiflow.IdentityServer.Core.Models;

namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthsController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthsController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("create-access-token")]
    public async Task<IActionResult> CreateAccessTokenByUser(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return Ok(tokenResponse);
    }

    [HttpPost("create-refresh-token")]
    public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(refreshToken, cancellationToken);
        return Ok(tokenResponse);
    }

    [HttpDelete("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshToken()
    {
        var token = await _authService.CreateTokenAsync();
        return Ok(token);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetAuthenticatedUser()
    {
        var token = await _authService.CreateTokenAsync();
        return Ok(token);
    }
}