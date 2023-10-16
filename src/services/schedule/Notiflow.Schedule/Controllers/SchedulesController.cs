namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class SchedulesController : MainController
{
    [HttpPost("text-message-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TextMessageDelivery([FromBody] ScheduleTextMessageRequest request, CancellationToken cancellationToken)
    {
        
    }
}
