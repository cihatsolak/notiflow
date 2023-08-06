namespace Notiflow.Backoffice.API.Controllers;

public sealed class EmailsController : BaseApiController
{
    /// <summary>
    /// Sends email to registered user | single send
    /// </summary>
    /// <response code="200">email/s sent</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="400">request is illegal</response>
    [HttpPost("send")]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Send([FromBody] SendEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return CreateOkResultInstance(response);
    }
}
