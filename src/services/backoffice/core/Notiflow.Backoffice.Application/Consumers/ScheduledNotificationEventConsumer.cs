namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledNotificationEventConsumer(ISender sender) : IConsumer<ScheduledNotificationEvent>
{
    public async Task Consume(ConsumeContext<ScheduledNotificationEvent> context)
    {
        var response = await sender.Send(new SendNotificationCommand(
            context.Message.CustomerIds,
            context.Message.ImageUrl,
            context.Message.Message,
            context.Message.Title
            ), context.CancellationToken);

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = !response.IsSuccess ? default : response.Message
        });
    }
}
