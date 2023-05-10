using Notiflow.IdentityServer.Service.TenantPermissions;

namespace Notiflow.IdentityServer.Controllers;

[Route("api/tenant-permissions")]
public sealed class TenantPermissionsController : MainController
{
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantPermissionsController(ITenantPermissionService tenantPermissionService)
    {
        _tenantPermissionService = tenantPermissionService;
    }

    /// <summary>
    /// Endpoint for retrieving permissions for a Tenant user
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized user</response>
    /// <response code="404">Permissions not found</response>
    [HttpGet("detail")]
    [ProducesResponseType(typeof(ResponseData<TenantPermissionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.GetPermissionsAsync(cancellationToken);
        return Ok(response);
    }

    /// <summary>
    /// Endpoint for updating the preferences of a Tenant user.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpPost("update-preferences")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePreferences([FromBody] TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.UpdateAsync(request, cancellationToken);
        return Ok(response);
    }
}
