namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class PlansController : BaseApiController
{
    private readonly ScheduledDbContext _context;

    public PlansController(ScheduledDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Schedules the delivery of a text message.
    /// </summary>
    /// <param name="request">The request containing the necessary information for the text message delivery.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>An Accepted result if the text message delivery is successfully scheduled.</returns>
    [HttpPost("text-message-delivery")]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TextMessageDelivery([FromBody] ScheduleTextMessageRequest request, CancellationToken cancellationToken)
    {
        await _context.ScheduledTextMessages.AddAsync(request.CreateScheduledTextMessage(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Status202Accepted(ResultCodes.TEXT_MESSAGE_SENDING_ACCEPTED);
        return CreateActionResultInstance(response);
    }

    /// <summary>
    /// Schedules the delivery of a notification.
    /// </summary>
    /// <param name="request">The request containing the necessary information for the notification delivery.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>An Accepted result if the notification delivery is successfully scheduled.</returns>
    [HttpPost("notification-delivery")]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> NotificationDelivery([FromBody] ScheduleNotificationRequest request, CancellationToken cancellationToken)
    {
        await _context.ScheduledNotifications.AddAsync(request.CreateScheduledNotification(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Status202Accepted(ResultCodes.NOTIFICATION_SENDING_ACCEPTED);
        return CreateActionResultInstance(response);
    }

    /// <summary>
    /// Schedules the delivery of an email.
    /// </summary>
    /// <param name="request">The request containing the necessary information for the email delivery.</param>
    /// <param name="cancellationToken">Propagates notification that operations should be canceled.</param>
    /// <returns>An Accepted result if the email delivery is successfully scheduled.</returns>
    [HttpPost("email-delivery")]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> EmailDelivery([FromBody] ScheduleEmailRequest request, CancellationToken cancellationToken)
    {
        await _context.ScheduledEmails.AddAsync(request.CreateScheduledEmail(), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Status202Accepted(ResultCodes.EMAIL_SENDING_ACCEPTED);
        return CreateActionResultInstance(response);
    }
}