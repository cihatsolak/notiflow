namespace Notiflow.IdentityServer.Controllers;

[Route("api/tenant-permissions")]
[ApiController]
public sealed class TenantPermissionsController : ControllerBase
{
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantPermissionsController(ITenantPermissionService tenantPermissionService)
    {
        _tenantPermissionService = tenantPermissionService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("detail")]
    [ProducesResponseType(typeof(ResponseModel<TenantPermissionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.GetPermissionsAsync(cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("update-preferences")]
    [ProducesResponseType(typeof(ResponseModel<int>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdatePreferences([FromBody] TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.UpdateAsync(request, cancellationToken);
        return Ok(response);
    }
}
