namespace Notiflow.Backoffice.API.Controllers;

public sealed class TextMessagesController : BaseApiController
{
    /// <summary>
    /// Lists history message detail by related id
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Text message history not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(Response<GetTextMessageHistoryByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Sends messages to customers
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Message could not be sent</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("send")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Send([FromBody] SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Ok(response);
    }
}
