namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// presents customer information
    /// </summary>
    /// <response code="200"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    public async Task<IActionResult> GetDetailById([FromRoute] GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    /// <summary>
    /// Add a new customer
    /// </summary>
    /// <response code="200"></response>
    /// <response code="401"></response>
    /// <response code="404"></response>
    [HttpPost("add")]
    public async Task<IActionResult> Add()
    {
        return Ok();
    }
}
