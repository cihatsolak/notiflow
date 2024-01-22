namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledTextMessageEventConsumer(ISender sender) : IConsumer<ScheduledTextMessageEvent>
{
    public async Task Consume(ConsumeContext<ScheduledTextMessageEvent> context)
    {
        var response = await sender.Send(new SendTextMessageCommand(context.Message.CustomerIds, context.Message.Message));

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = response.IsSuccess ? default : response.Message
        });
    }
}
