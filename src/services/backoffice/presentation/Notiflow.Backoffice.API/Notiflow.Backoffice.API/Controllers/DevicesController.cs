namespace Notiflow.Backoffice.API.Controllers;

public sealed class DevicesController : BaseApiController
{
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetDeviceById()
    {
        return Ok();
    }


    /// <summary>
    /// Adds a new device information of the customer
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status404NotFound)]
    [HttpPost("add")]
    public async Task<IActionResult> Add(InsertDeviceRequest insertDeviceRequest)
    {
        await Sender.Send(insertDeviceRequest);

        return CreatedAtAction(nameof(Add), new { id = 1 });
    }
}
