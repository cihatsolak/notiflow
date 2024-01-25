namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryTwice)]
public sealed class ScheduledEmailSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 2;

    private readonly ScheduledDbContext _context;
    private readonly IRequestClient<ScheduledEmailEvent> _client;
    private readonly ILogger<ScheduledEmailSendingRecurringJob> _logger;

    public ScheduledEmailSendingRecurringJob(
        ScheduledDbContext context, 
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
        var scheduledEmails = await _context.ScheduledEmails
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

            var response = await _client.GetResponse<ScheduledResponse>(scheduledEmail.Data.AsModel<ScheduledEmailEvent>());
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

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the emails planned to be sent has been completed.");
    }
}
