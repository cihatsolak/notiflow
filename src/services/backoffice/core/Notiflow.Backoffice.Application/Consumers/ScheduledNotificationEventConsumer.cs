namespace Notiflow.Backoffice.Application.Consumers;

public sealed class ScheduledNotificationEventConsumer : IConsumer<ScheduledNotificationEvent>
{
    private readonly IMediator _mediator;

    public ScheduledNotificationEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ScheduledNotificationEvent> context)
    {
        var response = await _mediator.Send(new SendNotificationCommand(
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
