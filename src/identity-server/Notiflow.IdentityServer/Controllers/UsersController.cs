namespace Notiflow.IdentityServer.Controllers;

[Route("api/[controller]")]
public sealed class UsersController : MainController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves detailed information about the user with the given ID.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpGet("{id:int:min(1)}/detail")]
    [ProducesResponseType(typeof(ResponseData<UserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDetail(int id, CancellationToken cancellationToken)
    {
        var userResponse = await _userService.GetDetailAsync(id, cancellationToken);
        return Ok(userResponse);
    }

    /// <summary>
    /// Adds a new user to the system.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpPost("add")]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var asd = await _userService.CreateAsync(request, cancellationToken);
        return Created("", asd);
    }

    /// <summary>
    /// Updates the information of the user with the given ID.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpPut("{id:int:min(1)}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await _userService.UpdateAsync(id, request, cancellationToken);
        return NoContent();
    }

    /// <summary>
    /// Deletes the user with the given ID.
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpDelete("{id:int:min(1)}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
