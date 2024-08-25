namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledEmailSendingRecurringJob(
    ScheduledDbContext context,
    IRequestClient<ScheduledEmailEvent> scheduledEmailClient,
    ILogger<ScheduledEmailSendingRecurringJob> logger)
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;
    private const int FIVE_MINUTES = 5;

    [JobDisplayName("[EMAIL] Sends scheduled emails.")]
    public async Task ExecuteAsync()
    {
        CancellationTokenSource cancellationTokenSource = new();
        cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(FIVE_MINUTES));

        try
        {
            var scheduledEmails = await context.ScheduledEmails
                .TagWith("Lists emails that are scheduled and waiting to be sent.")
                .Where(message => !message.IsSent &&
                                   message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                   message.PlannedDeliveryDate >= DateTime.UtcNow.AddMinutes(-15) &&
                                   message.PlannedDeliveryDate <= DateTime.UtcNow.AddMinutes(1))
                .ToListAsync(cancellationTokenSource.Token);

            if (scheduledEmails.IsNullOrNotAny())
                return;

            foreach (var scheduledEmail in scheduledEmails)
            {
                DateTime now = DateTime.UtcNow;

                var response = await scheduledEmailClient.GetResponse<ScheduledResponse>(
                    scheduledEmail.Data.AsModel<ScheduledEmailEvent>(),
                    cancellationTokenSource.Token
                );

                if (response.Message.Succeeded)
                {
                    scheduledEmail.IsSent = true;
                    scheduledEmail.SuccessDeliveryDate = now;
                    scheduledEmail.LastAttemptDate = now;
                }
                else
                {
                    scheduledEmail.FailedAttempts += 1;
                    scheduledEmail.ErrorMessage = response.Message.ErrorMessage;
                    scheduledEmail.LastAttemptDate = now;
                }
            }

            await context.SaveChangesAsync(cancellationTokenSource.Token);
            logger.LogInformation("The sending process of the emails planned to be sent has been completed.");
        }
        catch (OperationCanceledException ex)
        {
            logger.LogWarning(ex, "The operation was canceled due to timeout.");
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while sending scheduled emails.");
            throw;
        }
        finally
        {
            cancellationTokenSource.Dispose();
        }
    }
}
