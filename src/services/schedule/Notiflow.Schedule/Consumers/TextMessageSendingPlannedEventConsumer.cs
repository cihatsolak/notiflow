namespace Notiflow.Schedule.Consumers;

public sealed class TextMessageSendingPlannedEventConsumer : IConsumer<TextMessageSendingPlannedEvent>
{
    private readonly ScheduleDbContext _context;

    public TextMessageSendingPlannedEventConsumer(ScheduleDbContext context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<TextMessageSendingPlannedEvent> context)
    {
        
    }
}
