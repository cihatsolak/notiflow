namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryThreeTimes)]
public sealed class ScheduledTextMessageSendingRecurringJob(
    ScheduledDbContext context,
    IRequestClient<ScheduledTextMessageEvent> client,
    ILogger<ScheduledTextMessageSendingRecurringJob> logger)
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 3;
    private const int FIVE_MINUTES = 5;

    [JobDisplayName("[TEXT MESSAGE] Sends scheduled text messages.")]
    public async Task ExecuteAsync()
    {
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(FIVE_MINUTES));

        try
        {
            var scheduledTextMessages = await context.ScheduledTextMessages
           .TagWith("lists text messages that are scheduled and waiting to be sent.")
           .Where(message => !message.IsSent &&
                              message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                              message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                              message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
           .ToListAsync(cancellationTokenSource.Token);

            if (scheduledTextMessages.IsNullOrNotAny())
                return;

            foreach (var scheduledTextMessage in scheduledTextMessages)
            {
                DateTime now = DateTime.Now;

                var response = await client.GetResponse<ScheduledResponse>(scheduledTextMessage.Data.AsModel<ScheduledTextMessageEvent>(), cancellationTokenSource.Token);
                if (response.Message.Succeeded)
                {

                    scheduledTextMessage.IsSent = true;
                    scheduledTextMessage.SuccessDeliveryDate = now;
                    scheduledTextMessage.LastAttemptDate = now;
                }
                else
                {
                    scheduledTextMessage.FailedAttempts += 1;
                    scheduledTextMessage.ErrorMessage = response.Message.ErrorMessage;
                    scheduledTextMessage.LastAttemptDate = now;
                }
            }

            await context.SaveChangesAsync(cancellationTokenSource.Token);

            logger.LogInformation("The sending process of the text messages planned to be sent has been completed.");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "The operation was canceled due to timeout.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while sending scheduled text messages.");
            throw;
        }
        finally
        {
            cancellationTokenSource.Dispose();
        }
    }
}
