namespace Notiflow.Projections.EmailService.Consumers;

public sealed class EmailDeliveredEventConsumer : IConsumer<EmailDeliveredEvent>
{
    public Task Consume(ConsumeContext<EmailDeliveredEvent> context)
    {
        throw new NotImplementedException();
    }
}
