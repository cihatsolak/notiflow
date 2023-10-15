namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// Retrieves a DataTable of customer information based on the provided command.
    /// </summary>
    /// <param name="request">The command containing parameters for DataTable retrieval.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the DataTable result of customer information.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customers not found</response>
    [HttpPost("datatable")]
    [ProducesResponseType(typeof(ApiResponse<DtResult<CustomerDataTableCommandResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DataTable([FromBody] CustomerDataTableCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Get(response);
    }

    /// <summary>
    /// Retrieves detailed information about a customer based on its ID.
    /// </summary>
    /// <param name="request">The request containing the customer ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the detailed customer information.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customer information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(ApiResponse<GetCustomerByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Get(response);
    }

    /// <summary>
    /// Adds a new customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing customer details to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the ID of the added customer.</returns>
    /// <response code="201">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Created(response, nameof(GetById));
    }

    /// <summary>
    /// Updates an existing customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing customer details to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the customer update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPut("update")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.NoContent(response);
    }

    /// <summary>
    /// Deletes a customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing the customer ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the customer deletion operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.NoContent(response);
    }

    /// <summary>
    /// Updates the blocking status of a customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing customer ID and blocking status.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the customer blocking status update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-blocking")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBlocking([FromBody] UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.NoContent(response);
    }

    /// <summary>
    /// Updates the email of a customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing customer ID and new email.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the customer email update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-email")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.NoContent(response);
    }

    /// <summary>
    /// Updates the phone number of a customer based on the provided command.
    /// </summary>
    /// <param name="request">The command containing customer ID and new phone number.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the customer phone number update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-phone-number")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.NoContent(response);
    }
}
