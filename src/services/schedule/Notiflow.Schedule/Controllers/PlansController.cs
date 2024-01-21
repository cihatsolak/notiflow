namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class PlansController : MainController
{
    private readonly ScheduledDbContext _context;
    private readonly ILocalizerService<ResultMessage> _localizer;

    public PlansController(
        ScheduledDbContext context,
        ILocalizerService<ResultMessage> localizer)
    {
        _context = context;
        _localizer = localizer;
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
        ScheduledTextMessage scheduledTextMessage = new()
        {
            Data = request.ToScheduledTextMessageEvent(),
            PlannedDeliveryDate = request.PlannedDeliveryDate
        };

        await _context.ScheduledTextMessages.AddAsync(scheduledTextMessage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultMessage.TEXT_MESSAGE_SENDING_ACCEPTED]);
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
        ScheduledNotification scheduledNotification = new()
        {
            Data = request.ToScheduledNotificationEvent(),
            PlannedDeliveryDate = request.PlannedDeliveryDate
        };

        await _context.ScheduledNotifications.AddAsync(scheduledNotification, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultMessage.NOTIFICATION_SENDING_ACCEPTED]);
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
        ScheduledEmail scheduledEmail = new()
        {
            Data = request.ToScheduledEmailEvent(),
            PlannedDeliveryDate = request.PlannedDeliveryDate
        };

        await _context.ScheduledEmails.AddAsync(scheduledEmail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultMessage.EMAIL_SENDING_ACCEPTED]);
        return CreateActionResultInstance(response);
    }
}