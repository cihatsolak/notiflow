namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledNotificationSendingRecurringJob(
    ScheduledDbContext context,
    IRequestClient<ScheduledNotificationEvent> client,
    ILogger<ScheduledNotificationSendingRecurringJob> logger)
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;
    private const int FIVE_MINUTES = 5;

    [JobDisplayName("[NOTIFICATION] Sends scheduled notification.")]
    public async Task ExecuteAsync()
    {
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(FIVE_MINUTES));

        try
        {
            var scheduledNotifications = await context.ScheduledNotifications
              .TagWith("lists notifications that are scheduled and waiting to be sent.")
              .Where(message => !message.IsSent &&
                                 message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                 message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                                 message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
              .ToListAsync(cancellationTokenSource.Token);

            if (scheduledNotifications.IsNullOrNotAny())
                return;

            foreach (var scheduledNotification in scheduledNotifications)
            {
                DateTime now = DateTime.Now;

                var response = await client.GetResponse<ScheduledResponse>(scheduledNotification.Data.AsModel<ScheduledNotificationEvent>(), cancellationTokenSource.Token);
                if (response.Message.Succeeded)
                {
                    scheduledNotification.IsSent = true;
                    scheduledNotification.SuccessDeliveryDate = now;
                    scheduledNotification.LastAttemptDate = now;                   
                }
                else
                {
                    scheduledNotification.FailedAttempts += 1;
                    scheduledNotification.ErrorMessage = response.Message.ErrorMessage;
                    scheduledNotification.LastAttemptDate = now;
                }
            }

            await context.SaveChangesAsync(cancellationTokenSource.Token);

            logger.LogInformation("The sending process of the notifications planned to be sent has been completed.");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "The operation was canceled due to timeout.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while sending scheduled notifications.");
            throw;
        }
        finally
        {
            cancellationTokenSource.Dispose();
        }
    }
}
