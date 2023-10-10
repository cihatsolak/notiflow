namespace Puzzle.Lib.Response;

public static class Result
{
    [NonAction]
    public static IActionResult Get<T>(ApiResponse<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new NotFoundObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Ok<T>(ApiResponse<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new OkObjectResult(response);
    }

    [NonAction]
    public static IActionResult Created<T>(ApiResponse<T> response, string actionName)
    {
        ArgumentNullException.ThrowIfNull(response);
        ArgumentException.ThrowIfNullOrEmpty(actionName);
        
        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new CreatedAtActionResult(actionName, null, new { id = response.Data }, null);
    }

    [NonAction]
    public static IActionResult NoContent<T>(ApiResponse<T> response)
    {
        ArgumentNullException.ThrowIfNull(response);

        if (!response.Succeeded)
            return new BadRequestObjectResult(response);

        return new NoContentResult();
    }
}
