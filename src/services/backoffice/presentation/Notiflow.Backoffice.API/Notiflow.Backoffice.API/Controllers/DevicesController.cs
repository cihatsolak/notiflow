using Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;

namespace Notiflow.Backoffice.API.Controllers;

public sealed class DevicesController : BaseApiController
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    public async Task<IActionResult> GetDeviceById([FromRoute] GetDeviceByIdRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }


    /// <summary>
    /// Adds a new device information of the customer
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="400">request is illegal</response>
    /// <response code="401">unauthorized user</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody] AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(nameof(GetDeviceById), new { id = response.Data }, null);
    }

    /// <summary>
    /// Update device information
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPut("update")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }

    /// <summary>
    /// Delete current device
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }

    /// <summary>
    /// Update device token information
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-token")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateToken([FromBody] UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
