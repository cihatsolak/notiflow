using Notiflow.IdentityServer.Service.Auth;

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
    public async Task<IActionResult> CreateTokenByUser()
    {
        var token = await _authService.CreateTokenAsync();

        return Ok(token);
    }

    [HttpPost("create-refresh-token")]
    public async Task<IActionResult> CreateTokenByRefreshToken()
    {
        var token = await _authService.CreateTokenAsync();

        return Ok(token);
    }
}