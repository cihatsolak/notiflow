namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledTextMessageEventConsumer : IConsumer<ScheduledTextMessageEvent>
{
    private readonly IMediator _mediator;

    public ScheduledTextMessageEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ScheduledTextMessageEvent> context)
    {
        var response = await _mediator.Send(new SendTextMessageCommand
        {
            CustomerIds = context.Message.CustomerIds,
            Message = context.Message.Message
        });

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.IsSuccess,
            ErrorMessage = response.IsSuccess ? default : response.Message
        });
    }
}
