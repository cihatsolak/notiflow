namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledNotificationSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    private readonly ScheduledDbContext _context;
    private readonly IRequestClient<ScheduledNotificationEvent> _client;
    private readonly ILogger<ScheduledNotificationSendingRecurringJob> _logger;

    public ScheduledNotificationSendingRecurringJob(
        ScheduledDbContext context, 
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

        if (scheduledNotifications.IsNullOrNotAny())
            return;

        foreach (var scheduledNotification in scheduledNotifications)
        {
            DateTime now = DateTime.Now;

            var response = await _client.GetResponse<ScheduledResponse>(scheduledNotification.Data.AsModel<ScheduledNotificationEvent>());
            if (!response.Message.Succeeded)
            {
                scheduledNotification.FailedAttempts += 1;
                scheduledNotification.ErrorMessage = response.Message.ErrorMessage;
                scheduledNotification.LastAttemptDate = now;
            }
            else
            {
                scheduledNotification.IsSent = true;
                scheduledNotification.SuccessDeliveryDate = now;
                scheduledNotification.LastAttemptDate = now;
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the notifications planned to be sent has been completed.");
    }
}
