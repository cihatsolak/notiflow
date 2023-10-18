namespace Notiflow.Schedule.Jobs;

public sealed class ScheduledTextMessageSendingRecurringJob
{
    private const int MAXIMUM_FAILED_ATTEMPTS = 3;

    private readonly ScheduleDbContext _context;
    private readonly IRequestClient<ScheduledTextMessageSendEvent> _client;
    private readonly ILogger<ScheduledTextMessageSendingRecurringJob> _logger;

    public ScheduledTextMessageSendingRecurringJob(
        ScheduleDbContext context, 
        IRequestClient<ScheduledTextMessageSendEvent> client, 
        ILogger<ScheduledTextMessageSendingRecurringJob> logger)
    {
        _context = context;
        _client = client;
        _logger = logger;
    }

    public async Task ExecuteAsync()
    {
        try
        {
            _logger.LogInformation("The messages planned to be sent are started to be sent.");

            var scheduledTextMessages = await _context.ScheduledTextMessages
                .AsNoTracking()
                .Where(message => !message.IsSent &&
                                   message.FailedAttempts <= MAXIMUM_FAILED_ATTEMPTS &&
                                   string.IsNullOrEmpty(message.ErrorMessage) &&
                                   message.PlannedDeliveryDate >= DateTime.Now.AddMinutes(-15) &&
                                   message.PlannedDeliveryDate <= DateTime.Now.AddMinutes(1))
                .ToListAsync();

            foreach (var scheduledTextMessage in scheduledTextMessages)
            {
                ScheduledTextMessageSendEvent scheduledTextMessageSendEvent = scheduledTextMessage.Data.AsModel<ScheduledTextMessageSendEvent>();

                var scheduleEventResponse = await _client.GetResponse<ScheduleEventResponse>(scheduledTextMessageSendEvent, CancellationToken.None);
                if (!scheduleEventResponse.Message.Succeeded)
                {
                    scheduledTextMessage.FailedAttempts += 1;
                    scheduledTextMessage.ErrorMessage = scheduleEventResponse.Message.ErrorMessage;
                    scheduledTextMessage.LastAttemptDate = DateTime.Now;
                }
                else
                {
                    scheduledTextMessage.IsSent = true;
                    scheduledTextMessage.SuccessDeliveryDate = DateTime.Now;
                }
            }

            if (!scheduledTextMessages.IsNullOrNotAny())
            {
                _context.ScheduledTextMessages.UpdateRange(scheduledTextMessages);
                await _context.SaveChangesAsync();
            }

            _logger.LogInformation("The sending process of the messages planned to be sent has been completed.");
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
