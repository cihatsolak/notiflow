namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(Result<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class PlansController : MainController
{
    private readonly ScheduledDbContext _context;
    private readonly ILocalizerService<ResultState> _localizer;

    public PlansController(
        ScheduledDbContext context,
        ILocalizerService<ResultState> localizer)
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
        ScheduledTextMessageEvent @event = new()
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

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultState.TEXT_MESSAGE_SENDING_ACCEPTED]);
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
        ScheduledNotificationEvent @event = new()
        {
            CustomerIds = request.CustomerIds,
            Message = request.Message,
            ImageUrl = request.ImageUrl,
            Title = request.Title
        };

        ScheduledNotification scheduledNotification = new()
        {
            Data = @event.ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{request.Date} {request.Time}", CultureInfo.CurrentCulture)
        };

        await _context.ScheduledNotifications.AddAsync(scheduledNotification, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultState.NOTIFICATION_SENDING_ACCEPTED]);
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
        ScheduledEmailEvent @event = new()
        {
            Body = request.Body,
            Subject = request.Subject,
            CustomerIds = request.CustomerIds,
            CcAddresses = request.CcAddresses,
            BccAddresses = request.BccAddresses,
            IsBodyHtml = request.IsBodyHtml
        };

        ScheduledEmail scheduledEmail = new()
        {
            Data = @event.ToJson(),
            PlannedDeliveryDate = DateTime.Parse($"{request.Date} {request.Time}", CultureInfo.CurrentCulture)
        };

        await _context.ScheduledEmails.AddAsync(scheduledEmail, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var response = Result<EmptyResponse>.Success(StatusCodes.Status202Accepted, _localizer[ResultState.EMAIL_SENDING_ACCEPTED]);
        return CreateActionResultInstance(response);
    }
}