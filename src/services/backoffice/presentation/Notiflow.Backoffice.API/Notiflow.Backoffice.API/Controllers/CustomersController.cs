namespace Notiflow.Backoffice.API.Controllers;

public sealed class CustomersController : BaseApiController
{
    /// <summary>
    /// Lists customers in datatable format by pagination
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customers not found</response>
    [HttpPost("datatable")]
    [ProducesResponseType(typeof(Response<DtResult<CustomerDataTableResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DataTable([FromBody] CustomerDataTableCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Lists the customer's detail information
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customer information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response<GetCustomerByIdQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Add a new customer
    /// </summary>
    /// <response code="201">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Add([FromBody] AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Created(response, nameof(GetById));
    }

    /// <summary>
    /// Update current customer
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPut("update")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Delete current user
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Changes the customer's blocking status. blocked/unblocked.
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-blocking")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBlocking([FromBody] UpdateCustomerBlockingCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Changes the customer's email address
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-email")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateEmail([FromBody] UpdateCustomerEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Changes the customer's phone number
    /// </summary>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-phone-number")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePhoneNumber([FromBody] UpdateCustomerPhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }
}
