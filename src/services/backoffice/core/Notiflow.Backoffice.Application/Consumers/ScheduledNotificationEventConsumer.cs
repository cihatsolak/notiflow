using Notiflow.Backoffice.Application.Features.Commands.Notifications.SendMultiple;

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
        var response = await _mediator.Send(new SendMultipleNotificationCommand
        {
            CustomerIds = context.Message.CustomerIds,
            ImageUrl = context.Message.ImageUrl,
            Message = context.Message.Message,
            Title = context.Message.Title
        });

        await context.RespondAsync(new ScheduledResponse
        {
            Succeeded = response.Succeeded,
            ErrorMessage = response.Succeeded ? default : response.Message
        });
    }
}
