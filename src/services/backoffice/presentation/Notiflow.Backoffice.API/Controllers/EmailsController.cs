﻿namespace Notiflow.Backoffice.API.Controllers;

public sealed class EmailsController : BaseApiController
{
    /// <summary>
    /// Retrieves detailed information about an email history item based on its ID.
    /// </summary>
    /// <param name="request">The request containing the email history ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the email history details.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Device information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response<GetEmailHistoryByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetEmailHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Sends an email based on the provided email command.
    /// </summary>
    /// <param name="request">The command containing email details.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the email sending operation.</returns>
    /// <response code="200">email/s sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [Authorize(Policy = "EmailPermissionRestriction")]
    [HttpPost("send")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Send([FromBody] SendEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Ok(response);
    }
}