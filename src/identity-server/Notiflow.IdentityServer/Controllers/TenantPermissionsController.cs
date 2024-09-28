namespace Notiflow.IdentityServer.Controllers;

[Route("api/tenant-permissions")]
public sealed class TenantPermissionsController(ITenantPermissionService tenantPermissionService) : BaseApiController
{
    /// <summary>
    /// Retrieves the permissions for the current tenant.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the permissions for the current tenant.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="404">permissions not found</response>
    [HttpGet("detail")]
    [ProducesResponseType(typeof(Result<TenantPermissionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPermissions(CancellationToken cancellationToken)
    {
        var response = await tenantPermissionService.GetPermissionsAsync(cancellationToken);
        return CreateActionResultInstance(response);
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
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePreferences([FromBody] TenantPermissionRequest request, CancellationToken cancellationToken)
    {
        var response = await tenantPermissionService.UpdateAsync(request, cancellationToken);
        return CreateActionResultInstance(response);
    }
}
