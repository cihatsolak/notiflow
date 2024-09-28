namespace Notiflow.IdentityServer.Controllers;

public sealed class UsersController(IUserService userService) : BaseApiController
{
    /// <summary>
    /// Retrieves detailed information about a user based on the provided ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve details for.</param>s
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing detailed information about the user.</returns>
    /// <response code="200">operation successful</response>
    /// <response code="401">unauthorized user</response>
    /// <response code="404">user not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDetail(int id, CancellationToken cancellationToken)
    {
        var response = await userService.GetDetailAsync(id, cancellationToken);
        return CreateActionResultInstance(response);
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
    [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await userService.AddAsync(request, cancellationToken);
        return CreateActionResultInstance(response);
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
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var response = await userService.UpdateAsync(id, request, cancellationToken);
        return CreateActionResultInstance(response);
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
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await userService.DeleteAsync(id, cancellationToken);
        return CreateActionResultInstance(response);
    }

    /// <summary>
    /// Updates the profile photo for a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="profilePhoto">The new profile photo image to upload.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation if needed.</param>
    /// <returns>Returns a response indicating the success of the operation.</returns>
    /// <response code="204">Profile photo updated successfully.</response>
    /// <response code="400">Bad request. If the request is not valid or the update fails.</response>
    [HttpPatch("{id:int:min(1):max(2147483647)}/update-profile-photo")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProfilePhoto([FromForm] UpdateUserProfilePhotoRequest request, CancellationToken cancellationToken)
    {
        var response = await userService.UpdateProfilePhotoByIdAsync(request.Id, request.ProfilePhoto, cancellationToken);
        return CreateActionResultInstance(response);
    }
}
