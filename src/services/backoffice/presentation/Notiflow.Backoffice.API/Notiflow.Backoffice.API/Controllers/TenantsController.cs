namespace Notiflow.Backoffice.API.Controllers;

public sealed class TenantsController : BaseApiController
{
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    public async Task<IActionResult> Detail(int id, CancellationToken cancellationToken)
    {
        return Ok();
    }


    /// <summary>
    /// Adds a new tenant
    /// </summary>
    /// <response code="200">tenant added successfully</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">invalid request</response>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] AddTenantRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(Detail), new { id = response.Data });
    }
}
