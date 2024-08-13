namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledNotificationSendingRecurringJob(
    ScheduledDbContext context,
    IRequestClient<ScheduledNotificationEvent> client,
    ILogger<ScheduledNotificationSendingRecurringJob> logger)
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    [JobDisplayName("[NOTIFICATION] Sends scheduled notification.")]
    public async Task ExecuteAsync()
    {
        var scheduledNotifications = await context.ScheduledNotifications
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

            var response = await client.GetResponse<ScheduledResponse>(scheduledNotification.Data.AsModel<ScheduledNotificationEvent>());
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

        await context.SaveChangesAsync();

        logger.LogInformation("The sending process of the notifications planned to be sent has been completed.");
    }
}
