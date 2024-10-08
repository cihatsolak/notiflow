﻿namespace Puzzle.Lib.Response.Controllers;

/// <summary>
/// Represents the main controller for the application. Inherits from ControllerBase class.
/// Contains attributes for response content type, request content type, and HTTP status code for unauthorized and internal server errors.
/// </summary>
[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class MainController : ControllerBase
{
    [NonAction]
    public virtual IActionResult CreateActionResultInstance(Result result)
    {
        return new ObjectResult(result)
        {
            StatusCode = result.StatusCode,
        };
    }

    [NonAction]
    public virtual IActionResult CreateActionResultInstance<T>(Result<T> result)
    {
        return result.StatusCode switch
        {
            StatusCodes.Status201Created => Created(result.UrlAsCreated, result.Data),
            _ => new ObjectResult(result) { StatusCode = result.StatusCode }
        };
    }
}