namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledEmailEventConsumer : IConsumer<ScheduledEmailEvent>
{
    private readonly IMediator _mediator;

    public ScheduledEmailEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ScheduledEmailEvent> context)
    {
        var response = await _mediator.Send(new SendEmailCommand(context.Message.Body,
                                                                 context.Message.Subject,
                                                                 context.Message.CustomerIds,
                                                                 context.Message.CcAddresses,
                                                                 context.Message.BccAddresses,
                                                                 context.Message.IsBodyHtml));

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = response.IsSuccess ? default : response.Message
        });
    }
}
