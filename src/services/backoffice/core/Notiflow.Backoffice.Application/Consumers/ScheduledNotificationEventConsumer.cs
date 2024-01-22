namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledNotificationEventConsumer : IConsumer<ScheduledNotificationEvent>
{
    private readonly ISender _sender;
        
    public ScheduledNotificationEventConsumer(ISender sender)
    {
        _sender = sender;
    }

    public async Task Consume(ConsumeContext<ScheduledNotificationEvent> context)
    {
        var response = await _sender.Send(new SendNotificationCommand(
            context.Message.CustomerIds,
            context.Message.ImageUrl,
            context.Message.Message,
            context.Message.Title
            ));

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = response.IsSuccess ? default : response.Message
        });
    }
}
