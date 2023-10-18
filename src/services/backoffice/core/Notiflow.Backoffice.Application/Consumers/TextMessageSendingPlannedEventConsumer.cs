using Notiflow.Common.MessageBroker;

namespace Notiflow.Backoffice.Application.Consumers;

public sealed class TextMessageSendingPlannedEventConsumer : IConsumer<ScheduledTextMessageSendEvent>
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TextMessageSendingPlannedEventConsumer(IMediator mediator, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task Consume(ConsumeContext<ScheduledTextMessageSendEvent> context)
    {
        try
        {
            var response = await _mediator.Send(new SendTextMessageCommand
            {
                CustomerIds = context.Message.CustomerIds,
                Message = context.Message.Message
            });

            await context.RespondAsync(new ScheduleEventResponse
            {
                Succeeded = response.Succeeded,
                ErrorMessage = response.Succeeded ? default : response.Message
            });
        }
        catch (Exception ex)
        {

            throw;
        }
    }
}
