using Microsoft.AspNetCore.Authorization;

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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create-access-token")]
    [ProducesResponseType(typeof(ResponseModel<TokenResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAccessTokenByUser(CreateAccessTokenRequest request, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(request, cancellationToken);
        return Ok(tokenResponse);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("create-refresh-token")]
    [ProducesResponseType(typeof(ResponseModel<TokenResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var tokenResponse = await _authService.CreateAccessTokenAsync(refreshToken, cancellationToken);
        return Ok(tokenResponse);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="refreshToken"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("revoke-refresh-token/{refreshToken:length(44)}")]
    public async Task<IActionResult> RevokeRefreshToken(string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _authService.RevokeRefreshTokenAsync(refreshToken, cancellationToken);
        return Ok(token);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("user")]
    [Authorize]
    public async Task<IActionResult> GetAuthenticatedUser(CancellationToken cancellationToken)
    {
        var user = await _authService.GetAuthenticatedUserAsync(cancellationToken);
        return Ok(user);
    }
}