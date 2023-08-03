namespace Notiflow.Backoffice.API.Controllers;

public sealed class EmailsController : BaseApiController
{
    [HttpPost("send")]
    public async Task<IActionResult> Send(CancellationToken cancellationToken)
    {
        return Ok();
    }
}
