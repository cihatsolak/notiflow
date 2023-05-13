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
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseData<int>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody] AddDeviceRequest request)
    {
        var response = await Sender.Send(request);

        return CreatedAtAction(nameof(GetDeviceById), new { id = response.Data }, null);
    }
}
