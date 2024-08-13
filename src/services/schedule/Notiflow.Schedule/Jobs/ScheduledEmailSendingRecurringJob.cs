namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledEmailSendingRecurringJob(
    ScheduledDbContext context,
    IRequestClient<ScheduledEmailEvent> client,
    ILogger<ScheduledEmailSendingRecurringJob> logger)
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    [JobDisplayName("[EMAIL] Sends scheduled emails.")]
    public async Task ExecuteAsync()
    {
        var scheduledEmails = await context.ScheduledEmails
              .TagWith("lists emails that are scheduled and waiting to be sent.")
              .Where(message => !message.IsSent &&
                                 message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                 message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                                 message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
              .ToListAsync();

        if (scheduledEmails.IsNullOrNotAny())
            return;

        foreach (var scheduledEmail in scheduledEmails)
        {
            DateTime now = DateTime.Now;

            var response = await client.GetResponse<ScheduledResponse>(scheduledEmail.Data.AsModel<ScheduledEmailEvent>());
            if (!response.Message.Succeeded)
            {
                scheduledEmail.FailedAttempts += 1;
                scheduledEmail.ErrorMessage = response.Message.ErrorMessage;
                scheduledEmail.LastAttemptDate = now;
            }
            else
            {
                scheduledEmail.IsSent = true;
                scheduledEmail.SuccessDeliveryDate = DateTime.Now;
                scheduledEmail.LastAttemptDate = now;
            }
        }

        await context.SaveChangesAsync();

        logger.LogInformation("The sending process of the emails planned to be sent has been completed.");
    }
}
