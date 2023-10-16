using Notiflow.Backoffice.Application.Features.Commands.Schedules.TextMessageDelivery;

namespace Notiflow.Backoffice.API.Controllers;

public sealed class SchedulesController : BaseApiController
{
    [HttpPost("text-message-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TextMessageDelivery([FromBody] ScheduleTextMessageCommand request, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(request, cancellationToken);
        if (response.Succeeded)
        {
            return Accepted(response);
        }

        return BadRequest(response);
    }
}
