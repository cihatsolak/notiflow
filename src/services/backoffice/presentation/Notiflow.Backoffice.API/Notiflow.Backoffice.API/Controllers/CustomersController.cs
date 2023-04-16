namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    [HttpPost("add")]
    public async Task<IActionResult> Add()
    {
        return Ok();
    }
}
