using Notiflow.Backoffice.Application.Features.Commands.Customers.Add;
using Notiflow.Backoffice.Application.Features.Commands.Customers.Update;

namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// presents customer information
    /// </summary>
    /// <response code="200"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    public async Task<IActionResult> GetDetailById([FromRoute] GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    /// <summary>
    /// Add a new customer
    /// </summary>
    /// <response code="200"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCustomerRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetDetailById), new { id = response.Data }, null);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
