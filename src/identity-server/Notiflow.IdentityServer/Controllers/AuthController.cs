using Notiflow.IdentityServer.Service.Auth;

namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var token = _authService.CreateTokenAsync();

        return Ok(token);
    }
}