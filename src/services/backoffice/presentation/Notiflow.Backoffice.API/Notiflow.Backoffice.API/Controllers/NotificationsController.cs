namespace Notiflow.Backoffice.API.Controllers;

public sealed class NotificationsController : BaseApiController
{
    /// <summary>
    /// Lists history notification detail by related id
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Notification not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(Response<GetNotificationHistoryByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetNotificationHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Sends notification to registered user | single send
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [HttpPost("send-single")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendSingle([FromBody] SendSingleNotificationCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Ok(response);
    }

    /// <summary>
    /// Sends notification to registered users | multiple send
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [HttpPost("send-multiple")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendMultiple([FromBody] SendMultipleNotificationCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Ok(response);
    }
}
