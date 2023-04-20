﻿namespace Notiflow.Backoffice.API.Controllers;

public sealed class NotificationsController : BaseApiController
{
    /// <summary>
    /// Sends notification to registered user
    /// </summary>
    /// <response code="200">notification sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseModel<Unit>), StatusCodes.Status404NotFound)]
    [HttpPost("send")]
    public async Task<IActionResult> SendNotification([FromBody] SendNotificationRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Ok(response);
    }
}
