namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryThreeTimes)]
public sealed class ScheduledTextMessageSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 3;

    private readonly ScheduledDbContext _context;
    private readonly IRequestClient<ScheduledTextMessageEvent> _client;
    private readonly ILogger<ScheduledTextMessageSendingRecurringJob> _logger;

    public ScheduledTextMessageSendingRecurringJob(
        ScheduledDbContext context,
        IRequestClient<ScheduledTextMessageEvent> client,
        ILogger<ScheduledTextMessageSendingRecurringJob> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }

    [JobDisplayName("[TEXT MESSAGE] Sends scheduled text messages.")]
    public async Task ExecuteAsync()
    {
        _logger.LogInformation("The text messages planned to be sent are started to be sent.");

        var scheduledTextMessages = await _context.ScheduledTextMessages
            .TagWith("lists text messages that are scheduled and waiting to be sent.")
            .Where(message => !message.IsSent &&
                               message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                               message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                               message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
            .ToListAsync();

        if (scheduledTextMessages.IsNullOrNotAny())
            return;

        foreach (var scheduledTextMessage in scheduledTextMessages)
        {
            DateTime now = DateTime.Now;

            var response = await _client.GetResponse<ScheduledResponse>(scheduledTextMessage.Data.AsModel<ScheduledTextMessageEvent>());
            if (!response.Message.Succeeded)
            {
                scheduledTextMessage.FailedAttempts += 1;
                scheduledTextMessage.ErrorMessage = response.Message.ErrorMessage;
                scheduledTextMessage.LastAttemptDate = now;
            }
            else
            {
                scheduledTextMessage.IsSent = true;
                scheduledTextMessage.SuccessDeliveryDate = now;
                scheduledTextMessage.LastAttemptDate = now;
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the text messages planned to be sent has been completed.");
    }
}
