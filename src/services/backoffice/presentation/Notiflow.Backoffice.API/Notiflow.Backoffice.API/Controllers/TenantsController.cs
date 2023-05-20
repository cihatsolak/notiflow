namespace Notiflow.Backoffice.API.Controllers;

public sealed class TenantsController : BaseApiController
{
    /// <summary>
    /// Lists the tenant's detail information
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Tenant information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response<GetTenantDetailByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetailById([FromRoute] GetTenantDetailByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return CreateGetResultInstance(response);
    }


    /// <summary>
    /// Adds a new tenant
    /// </summary>
    /// <response code="200">tenant added successfully</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">invalid request</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody] AddTenantCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return CreateCreatedResultInstance(response, nameof(GetDetailById));
    }
}
