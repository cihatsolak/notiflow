namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledEmailEventConsumer(ISender sender) : IConsumer<ScheduledEmailEvent>
{
    public async Task Consume(ConsumeContext<ScheduledEmailEvent> context)
    {
        var response = await sender.Send(new SendEmailCommand(context.Message.Body,
                                                                 context.Message.Subject,
                                                                 context.Message.CustomerIds,
                                                                 context.Message.CcAddresses,
                                                                 context.Message.BccAddresses,
                                                                 context.Message.IsBodyHtml), context.CancellationToken);

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = response.IsSuccess ? default : response.Message
        });
    }
}
