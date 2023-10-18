namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledNotificationSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    private readonly ScheduleDbContext _context;
    private readonly IRequestClient<ScheduledNotificationEvent> _client;
    private readonly ILogger<ScheduledNotificationSendingRecurringJob> _logger;

    public ScheduledNotificationSendingRecurringJob(
        ScheduleDbContext context, 
        IRequestClient<ScheduledNotificationEvent> client, 
        ILogger<ScheduledNotificationSendingRecurringJob> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }

    [JobDisplayName("[NOTIFICATION] Sends scheduled notification.")]
    public async Task ExecuteAsync()
    {
        _logger.LogInformation("The notification planned to be sent are started to be sent.");

        var scheduledNotifications = await _context.ScheduledNotifications
               .TagWith("lists notifications that are scheduled and waiting to be sent.")
               .Where(message => !message.IsSent &&
                                  message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                  message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                                  message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
               .ToListAsync();

        if (!scheduledNotifications.IsNullOrNotAny())
        {
            return;
        }

        foreach (var scheduledNotification in scheduledNotifications)
        {
            var scheduledNotificationEvent = scheduledNotification.Data.AsModel<ScheduledNotificationEvent>();

            var scheduledResponse = await _client.GetResponse<ScheduledResponse>(scheduledNotificationEvent);
            if (!scheduledResponse.Message.Succeeded)
            {
                scheduledNotification.FailedAttempts += 1;
                scheduledNotification.ErrorMessage = scheduledResponse.Message.ErrorMessage;
                scheduledNotification.LastAttemptDate = DateTime.Now;
            }
            else
            {
                scheduledNotification.IsSent = true;
                scheduledNotification.SuccessDeliveryDate = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the notifications planned to be sent has been completed.");
    }
}
