﻿namespace Notiflow.IdentityServer.Controllers;

public sealed class UsersController : BaseApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Retrieves detailed information about a user based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve details for.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing detailed information about the user.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="404">user not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id, CancellationToken cancellationToken)
    {
        var response = await _userService.GetDetailAsync(id, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Creates a new user based on the provided request.
    /// </summary>
    /// <param name="request">The request containing information for creating a new user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the result of the user creation operation.</returns>
    /// <response code="201">operation successful</response>
    /// <response code="400">invalid request</response>
    /// <response code="401">unauthorized user</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.AddAsync(request, cancellationToken);
        return HttpResult.Created(response, nameof(GetDetail));
    }

    /// <summary>
    /// Updates an existing user based on the provided request.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The request containing updated information for the user.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the user update operation.</returns>
    /// <response code="204">operation successful</response>
    /// <response code="400">invalid request</response>
    /// <response code="401">unauthorized user</response>
    [HttpPut("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await _userService.UpdateAsync(id, request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Deletes a user based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the user deletion operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Invalid request</response>
    /// <response code="401">Unauthorized user</response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await _userService.DeleteAsync(id, cancellationToken);
        return HttpResult.NoContent(response);
    }
}
