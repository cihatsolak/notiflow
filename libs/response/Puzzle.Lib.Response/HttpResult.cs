namespace Puzzle.Lib.Response;

public static class HttpResult
{
    [NonAction]
    public static IActionResult Get<T>(Response<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new NotFoundObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Ok<T>(Response<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Created<T>(Response<T> response, string actionName)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentException.ThrowIfNullOrEmpty(actionName);
        
        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new CreatedAtActionResult(actionName, null, new { id = response.Data }, null);
    }

    [NonAction]
    public static IActionResult NoContent<T>(Response<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new NoContentResult();
    }
}
