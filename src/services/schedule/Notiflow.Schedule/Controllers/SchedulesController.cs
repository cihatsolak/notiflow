﻿namespace Notiflow.Schedule.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status500InternalServerError)]
public sealed class SchedulesController : MainController
{
    private readonly ScheduledDbContext _context;

    public SchedulesController(ScheduledDbContext context)
    {
        _context = context;
    }

    [HttpPost("text-message-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
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

        return Accepted();
    }

    [HttpPost("notification-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
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

        return Accepted();
    }

    [HttpPost("email-delivery")]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status400BadRequest)]
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

        return Accepted();
    }
}
