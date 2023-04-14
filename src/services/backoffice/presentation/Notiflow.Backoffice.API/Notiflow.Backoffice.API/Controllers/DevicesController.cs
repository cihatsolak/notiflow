namespace Notiflow.Backoffice.API.Controllers;

public sealed class DevicesController : BaseApiController
{
    /// <summary>
    /// Adds a new device information of the customer
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status404NotFound)]
    [HttpPost("insert")]
    public async Task<IActionResult> Insert(InsertDeviceRequest insertDeviceRequest)
    {
        await Sender.Send(insertDeviceRequest);

        return CreatedAtAction(nameof(Insert), new { id = 1 });
    }
}
