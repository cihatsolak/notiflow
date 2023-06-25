namespace Puzzle.Lib.Response.Controllers;

public class ResultController : MainController
{
    protected virtual IActionResult CreateGetResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    protected virtual IActionResult CreateOkResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return Ok(response);
    }

    protected virtual IActionResult CreateCreatedResultInstance<T>(Response<T> response, string actionName)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return CreatedAtAction(actionName, new { id = response.Data }, null);
    }

    protected virtual IActionResult CreateNoContentResultInstance<T>(Response<T> response)
    {
        if (!response.Succeeded)
        {
            return BadRequest(response);
        }

        return NoContent();
    }
}
