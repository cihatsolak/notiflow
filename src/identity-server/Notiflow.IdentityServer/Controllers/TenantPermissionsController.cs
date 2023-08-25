namespace Notiflow.IdentityServer.Controllers;

[Route("api/tenant-permissions")]
public sealed class TenantPermissionsController : BaseApiController
{
    private readonly ITenantPermissionService _tenantPermissionService;

    public TenantPermissionsController(ITenantPermissionService tenantPermissionService)
    {
        _tenantPermissionService = tenantPermissionService;
    }

    /// <summary>
    /// Retrieves the permissions for the current tenant.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the permissions for the current tenant.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="404">permissions not found</response>
    [HttpGet("detail")]
    [ProducesResponseType(typeof(Response<TenantPermissionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.GetPermissionsAsync(cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Updates the preferences and permissions for the current tenant.
    /// </summary>
    /// <param name="request">The request containing the updated preferences and permissions.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the preferences and permissions update.</returns>
    /// <response code="204">operation successful</response>
    /// <response code="400">invalid request</response>
    /// <response code="401">unauthorized user</response>
    [HttpPut("update-preferences")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePreferences([FromBody] TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await _tenantPermissionService.UpdateAsync(request, cancellationToken);
        return HttpResult.NoContent(response);
    }
}
