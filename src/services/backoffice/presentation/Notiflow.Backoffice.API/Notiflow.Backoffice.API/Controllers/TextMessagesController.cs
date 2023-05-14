namespace Notiflow.Backoffice.API.Controllers;

public sealed class TextMessagesController : BaseApiController
{
    /// <summary>
    /// Sends a message to a single customer
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Message could not be sent</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("send-single")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendSingle([FromBody] SendSingleTextMessageRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    /// <summary>
    /// Sends messages to multiple customers
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Messages could not be sent</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("send-multiple")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SendMultiple([FromBody] SendMultipleTextMessageRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }
}
