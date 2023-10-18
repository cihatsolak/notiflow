namespace Notiflow.Schedule.Jobs;

[AutomaticRetry(Attempts = Attempts.TryThreeTimes)]
public sealed class ScheduledTextMessageSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 3;

    private readonly ScheduleDbContext _context;
    private readonly IRequestClient<ScheduledTextMessageEvent> _client;
    private readonly ILogger<ScheduledTextMessageSendingRecurringJob> _logger;

    public ScheduledTextMessageSendingRecurringJob(
        ScheduleDbContext context, 
        IRequestClient<ScheduledTextMessageEvent> client, 
        ILogger<ScheduledTextMessageSendingRecurringJob> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }

    [JobDisplayName("[TEXT-MESSAGE] Sends scheduled text messages.")]
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

        if (!scheduledTextMessages.IsNullOrNotAny())
        {
            return;
        }

        foreach (var scheduledTextMessage in scheduledTextMessages)
        {
            var scheduledTextMessageEvent = scheduledTextMessage.Data.AsModel<ScheduledTextMessageEvent>();

            var scheduledResponse = await _client.GetResponse<ScheduledResponse>(scheduledTextMessageEvent, CancellationToken.None);
            if (!scheduledResponse.Message.Succeeded)
            {
                scheduledTextMessage.FailedAttempts += 1;
                scheduledTextMessage.ErrorMessage = scheduledResponse.Message.ErrorMessage;
                scheduledTextMessage.LastAttemptDate = DateTime.Now;
            }
            else
            {
                scheduledTextMessage.IsSent = true;
                scheduledTextMessage.SuccessDeliveryDate = DateTime.Now;
            }
        }

        await _context.SaveChangesAsync();

        _logger.LogInformation("The sending process of the text messages planned to be sent has been completed.");
    }
}
