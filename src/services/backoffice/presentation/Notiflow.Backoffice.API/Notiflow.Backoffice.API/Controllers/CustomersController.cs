namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// presents customer information
    /// </summary>
    /// <response code="200"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpGet("{id}/detail")]
    public async Task<IActionResult> GetDetailById([FromRoute] GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add()
    {
        return Ok();
    }
}
