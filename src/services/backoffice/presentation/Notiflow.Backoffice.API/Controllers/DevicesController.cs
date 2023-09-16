﻿namespace Notiflow.Backoffice.API.Controllers;

public sealed class DevicesController : BaseApiController
{
    /// <summary>
    /// Retrieves a DataTable of device information based on the provided command.
    /// </summary>
    /// <param name="request">The command containing parameters for DataTable retrieval.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the DataTable result of device information.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Devices not found</response>
    [HttpPost("datatable")]
    [ProducesResponseType(typeof(Response<DtResult<DeviceDataTableResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DataTable([FromBody] DeviceDataTableCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Retrieves detailed information about a device based on its ID.
    /// </summary>
    /// <param name="request">The request containing the device ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the detailed device information.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Device information not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}/detail")]
    [ProducesResponseType(typeof(Response<GetDeviceByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetDeviceByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Get(response);
    }

    /// <summary>
    /// Adds a new device based on the provided command.
    /// </summary>
    /// <param name="request">The command containing device details to add.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the ID of the added device.</returns>
    /// <response code="201">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Response<int>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Add([FromBody] AddDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.Created(response, nameof(GetById));
    }

    /// <summary>
    /// Updates an existing device based on the provided command.
    /// </summary>
    /// <param name="request">The command containing device details to update.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the device update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPut("update")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Deletes a device based on the provided command.
    /// </summary>
    /// <param name="request">The command containing the device ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the device deletion operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpDelete("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] DeleteDeviceCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }

    /// <summary>
    /// Updates the token of a device based on the provided command.
    /// </summary>
    /// <param name="request">The command containing the device ID and new token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the device token update operation.</returns>
    /// <response code="204">Operation successful</response>
    /// <response code="400">Operation failed</response>
    /// <response code="401">Unauthorized action</response>
    [HttpPatch("update-token")]
    [ProducesResponseType(typeof(NoContentResult), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(Response<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateToken([FromBody] UpdateDeviceTokenCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return HttpResult.NoContent(response);
    }
}