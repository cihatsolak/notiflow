namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class SchedulesController : MainController
{
    private readonly ScheduleDbContext _context;

    public SchedulesController(ScheduleDbContext context)
    {
        _context = context;
    }

    [HttpPost("text-message-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TextMessageDelivery([FromBody] ScheduleTextMessageRequest request, CancellationToken cancellationToken)
    {
        ScheduledTextMessageSendEvent @event = new()
        {
            CustomerIds = request.CustomerIds,
            Message = request.Message
        };

        ScheduledTextMessage scheduledTextMessage = new()
        {
            Data = @event.ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{request.Date} {request.Time}", CultureInfo.CurrentCulture)
        };

        await _context.ScheduledTextMessages.AddAsync(scheduledTextMessage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Accepted();
    }
}
