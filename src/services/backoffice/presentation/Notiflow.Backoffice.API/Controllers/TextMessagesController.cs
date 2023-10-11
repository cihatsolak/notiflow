namespace Notiflow.Backoffice.API.Controllers;

[Route("api/text-messages")]
public sealed class TextMessagesController : BaseApiController
{
    /// <summary>
    /// Retrieves a data table of text message records based on the provided command.
    /// </summary>
    /// <param name="request">The command containing filtering and pagination parameters.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the data table of text message records.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Customers not found</response>
    [HttpPost("datatable")]
    [ProducesResponseType(typeof(ApiResponse<DtResult<TextMessageDataTableCommandResult>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DataTable([FromBody] TextMessageDataTableCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Get(response);
    }

    /// <summary>
    /// Retrieves text message history based on the provided ID.
    /// </summary>
    /// <param name="request">The request containing the text message history ID.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response containing the text message history details.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">Text message history not found</response>
    [HttpGet("{id:int:min(1):max(2147483647)}")]
    [ProducesResponseType(typeof(ApiResponse<GetTextMessageHistoryByIdQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] GetTextMessageHistoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Get(response);
    }

    /// <summary>
    /// Sends a text message based on the provided command.
    /// </summary>
    /// <param name="request">The command containing text message details to send.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The response indicating the result of the text message sending operation.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="400">Message could not be sent</response>
    /// <response code="401">Unauthorized action</response>
    [Authorize(Policy = PolicyName.TEXT_MESSAGE_PERMISSON_RESTRICTION)]
    [HttpPost("send")]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Unit>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Send([FromBody] SendTextMessageCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        return Result.Ok(response);
    }
}
