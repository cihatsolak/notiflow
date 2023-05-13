﻿namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// Lists the customer's detail information
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customer information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
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
    /// <response code="201"></response>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("update")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <response code="204"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
