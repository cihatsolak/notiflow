namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
public class BaseApiController : MainController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult CreateGetResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    protected IActionResult CreateOkResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    protected IActionResult CreateCreatedResultInstance<T>(Response<T> response, string actionName)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(actionName, new { id = response.Data }, null);
    }

    protected IActionResult CreateNoContentResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
