namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledEmailSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    private readonly ScheduleDbContext _context;
    private readonly IRequestClient<ScheduledEmailEvent> _client;
    private readonly ILogger<ScheduledEmailSendingRecurringJob> _logger;

    public ScheduledEmailSendingRecurringJob(
        ScheduleDbContext context, 
        IRequestClient<ScheduledEmailEvent> client, 
        ILogger<ScheduledEmailSendingRecurringJob> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }

    [JobDisplayName("[EMAIL] Sends scheduled emails.")]
    public async Task ExecuteAsync()
    {
        _logger.LogInformation("The emails planned to be sent are started to be sent.");

        var scheduledEmails = await _context.ScheduledEmails
              .TagWith("lists emails that are scheduled and waiting to be sent.")
              .Where(message => !message.IsSent &&
                                 message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                 message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                                 message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
              .ToListAsync();

        if (!scheduledEmails.IsNullOrNotAny())
        {
            return;
        }

        foreach (var scheduledEmail in scheduledEmails)
        {
            var scheduledNotificationEvent = scheduledEmail.Data.AsModel<ScheduledEmailEvent>();

            var scheduledResponse = await _client.GetResponse<ScheduledResponse>(scheduledNotificationEvent);
            if (!scheduledResponse.Message.Succeeded)
            {
                scheduledEmail.FailedAttempts += 1;
                scheduledEmail.ErrorMessage = scheduledResponse.Message.ErrorMessage;
                scheduledEmail.LastAttemptDate = DateTime.Now;
            }
            else
            {
                scheduledEmail.IsSent = true;
                scheduledEmail.SuccessDeliveryDate = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the emails planned to be sent has been completed.");
    }
}
