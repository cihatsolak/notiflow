namespace Puzzle.Lib.Response;

public static class ResultT
{
    [NonAction]
    public static IActionResult Get<T>(Result<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new NotFoundObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Ok<T>(Result<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Created<T>(Result<T> response, string actionName)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentException.ThrowIfNullOrEmpty(actionName);
        
        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new CreatedAtActionResult(actionName, null, new { id = response.Data }, null);
    }

    [NonAction]
    public static IActionResult NoContent<T>(Result<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new NoContentResult();
    }
}
